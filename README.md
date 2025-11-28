# LLMSharp

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**A full-featured, industrial-grade .NET LLM SDK following The Standard**

Designed by **Zafar Urakov**, LLMSharp is a generic, extensible, pluggable, clean, SOLID, and testable library for working with Large Language Models in .NET applications.

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Quick Start](#quick-start)
- [Usage Examples](#usage-examples)
- [Project Structure](#project-structure)
- [Extending LLMSharp](#extending-llmsharp)
- [Testing](#testing)
- [Build Instructions](#build-instructions)
- [Contributing](#contributing)
- [License](#license)

---

## Overview

LLMSharp follows **The Standard** by Hassan Habib, implementing a clean, layered architecture that promotes:

- **Separation of Concerns**: Clear boundaries between layers
- **Testability**: Every component can be unit tested in isolation
- **Extensibility**: Plug in custom providers, stores, and model engines
- **SOLID Principles**: Single responsibility, open/closed, dependency inversion
- **Generic Design**: Not tied to any specific LLM framework

---

## Architecture

LLMSharp follows a strict layered architecture:

```
┌─────────────────────────────────────┐
│         CLIENT LAYER                │  ◄── User's entry point
│  (ChatClient, VectorClient, etc.)   │
└─────────────────────────────────────┘
            ▼
┌─────────────────────────────────────┐
│    ORCHESTRATION SERVICES LAYER     │  ◄── Coordinates multiple services
│    (ChatOrchestrationService)       │
└─────────────────────────────────────┘
            ▼
┌─────────────────────────────────────┐
│     FOUNDATION SERVICES LAYER       │  ◄── Core business logic
│ (ChatService, EmbeddingService...)  │
└─────────────────────────────────────┘
            ▼
┌─────────────────────────────────────┐
│          BROKERS LAYER              │  ◄── External dependencies
│ (ModelEngine, File, Logging...)     │
└─────────────────────────────────────┘
            ▼
┌─────────────────────────────────────┐
│       MODELS & PROVIDERS            │  ◄── Data structures & plugins
└─────────────────────────────────────┘
```

### Layer Responsibilities

#### 1. **Brokers Layer** (`LLMSharp.Brokers.*`)
   - Lowest level of abstraction
   - Wraps external dependencies (file system, model engines, databases, time, etc.)
   - Makes code testable by isolating external calls
   - Examples: `FileBroker`, `ModelEngineBroker`, `MonitoringBroker`

#### 2. **Models Layer** (`LLMSharp.Models.*`)
   - Pure data structures
   - Contains requests, responses, configurations, exceptions
   - Interfaces for extensibility (`IDatasetProvider`, `IVectorStore`)

#### 3. **Foundation Services Layer** (`LLMSharp.Services.Foundations.*`)
   - Single-responsibility services
   - Each service handles one specific domain (Chat, Embeddings, Training, etc.)
   - Depends only on brokers
   - Examples: `ChatService`, `EmbeddingService`, `ModelLoadingService`

#### 4. **Orchestration Services Layer** (`LLMSharp.Services.Orchestrations.*`)
   - Coordinates multiple foundation services
   - Adds cross-cutting concerns (monitoring, logging, validation)
   - Example: `ChatOrchestrationService` combines chat + monitoring

#### 5. **Client Layer** (`LLMSharp.Clients.*`)
   - **User-facing API** - the main entry point
   - Provides clean, simple methods for end users
   - Examples: `ChatClient`, `VectorClient`
   - Accessed via `LlmClient` facade

#### 6. **Providers** (`LLMSharp.Providers.*`)
   - Pluggable implementations of `IDatasetProvider` and `IVectorStore`
   - Built-in: InMemory, SQLite, SQL Server, MongoDB, File-based
   - Users can create custom providers

---

## Features

### Core Capabilities

- ✅ **Model Loading**: Load local GGUF/ONNX models (extensible to remote models)
- ✅ **Chat**: Synchronous and streaming chat completions
- ✅ **Embeddings**: Generate and store vector embeddings
- ✅ **Vector Search**: Semantic search using vector stores
- ✅ **Training & Fine-Tuning**: Architecture for dataset loading, training pipelines, evaluation
- ✅ **Monitoring & Observability**: Track all interactions with detailed metrics
- ✅ **Pluggable Providers**: Swap dataset and vector store implementations

### Dataset Providers (Pluggable)

- `InMemoryDatasetProvider` (built-in)
- `SqlLocalDbProvider` (planned)
- `SqliteProvider` (planned)
- `MongoDbProvider` (planned)
- `FileSystemProvider` (JSON, CSV, TXT) (planned)
- Custom providers via `IDatasetProvider`

### Vector Stores (Pluggable)

- `InMemoryVectorStore` (built-in)
- `SqliteVectorStore` (planned)
- `FileBasedVectorStore` (planned)
- Custom stores via `IVectorStore`

### Monitoring

Every operation can be tracked:
- Timestamps
- Duration
- Token counts
- Model information
- Custom metadata

---

## Quick Start

### Installation

```bash
dotnet add package LLMSharp
```

### Basic Usage

```csharp
using LLMSharp.Clients;
using LLMSharp.Configurations;

// Configure and build the LLM client
LlmClient llm = await LlmConfigurator
    .Configure()
    .UseModel("path/to/model.gguf")
    .EnableMonitoring(true)
    .BuildAsync();

// Chat
var response = await llm.Chat.ChatAsync("Hello, how are you?");
Console.WriteLine(response.Content);
```

---

## Usage Examples

### 1. Simple Chat

```csharp
LlmClient llm = await LlmConfigurator
    .Configure()
    .UseModel("mymodel.gguf")
    .BuildAsync();

string answer = (await llm.Chat.ChatAsync("What is AI?")).Content;
Console.WriteLine(answer);
```

### 2. Advanced Chat with Options

```csharp
var request = new ChatRequest
{
    Prompt = "Explain quantum computing",
    MaxTokens = 500,
    Temperature = 0.8,
    TopP = 0.95,
    SystemMessage = "You are a helpful physics tutor."
};

ChatResponse response = await llm.Chat.ChatAsync(request);

Console.WriteLine($"Response: {response.Content}");
Console.WriteLine($"Tokens used: {response.TokenCount}");
Console.WriteLine($"Duration: {response.Duration.TotalSeconds}s");
```

### 3. Embeddings and Vector Search

```csharp
// Store documents as vectors
await llm.Vectors.SaveEmbeddingAsync("doc1", "Machine learning is a type of AI");
await llm.Vectors.SaveEmbeddingAsync("doc2", "Neural networks power deep learning");
await llm.Vectors.SaveEmbeddingAsync("doc3", "The sky is blue");

// Search for similar documents
var results = await llm.Vectors.SearchSimilarAsync("Tell me about AI", topK: 2);

foreach (var key in results)
{
    Console.WriteLine($"Similar document: {key}");
}
```

### 4. Using Custom Dataset Provider

```csharp
using LLMSharp.Models.Datasets;
using LLMSharp.Providers.Datasets.SqlServer;

IDatasetProvider customProvider = new SqlLocalDbProvider(
    "Server=(localdb)\\MSSQLLocalDB;Database=MyLlmData;"
);

LlmClient llm = await LlmConfigurator
    .Configure()
    .UseModel("model.gguf")
    .UseDatasetProvider(customProvider)
    .BuildAsync();
```

### 5. Using Custom Vector Store

```csharp
using LLMSharp.Models.VectorStores;
using LLMSharp.Providers.VectorStores.Sqlite;

IVectorStore vectorStore = new SqliteVectorStore("vectors.db");

LlmClient llm = await LlmConfigurator
    .Configure()
    .UseModel("model.gguf")
    .UseVectorStore(vectorStore)
    .BuildAsync();
```

---

## Project Structure

```
LLMSharp/
│
├── LLMSharp/                          # Main library
│   ├── Brokers/                       # External dependency wrappers
│   │   ├── Files/
│   │   ├── ModelEngines/
│   │   ├── Loggings/
│   │   ├── Monitorings/
│   │   ├── Times/
│   │   ├── Datasets/
│   │   └── VectorStores/
│   │
│   ├── Models/                        # Data structures
│   │   ├── Chats/
│   │   ├── Embeddings/
│   │   ├── Trainings/
│   │   ├── Monitorings/
│   │   ├── ModelEngines/
│   │   ├── Datasets/
│   │   ├── VectorStores/
│   │   ├── Exceptions/
│   │   └── Streamings/
│   │
│   ├── Services/
│   │   ├── Foundations/               # Single-responsibility services
│   │   │   ├── Chats/
│   │   │   ├── Embeddings/
│   │   │   ├── ModelLoadings/
│   │   │   ├── Trainings/
│   │   │   ├── Evaluations/
│   │   │   └── VectorStores/
│   │   │
│   │   └── Orchestrations/            # Multi-service coordination
│   │       ├── Chats/
│   │       ├── Trainings/
│   │       └── Embeddings/
│   │
│   ├── Clients/                       # ⭐ User-facing API
│   │   ├── Chats/
│   │   │   ├── IChatClient.cs
│   │   │   └── ChatClient.cs
│   │   ├── Vectors/
│   │   │   ├── IVectorClient.cs
│   │   │   └── VectorClient.cs
│   │   └── LlmClient.cs               # Main client facade
│   │
│   ├── Providers/                     # Pluggable implementations
│   │   ├── Datasets/
│   │   │   ├── InMemory/
│   │   │   ├── SqlServer/
│   │   │   ├── Sqlite/
│   │   │   ├── MongoDb/
│   │   │   └── FileSystem/
│   │   │
│   │   └── VectorStores/
│   │       ├── InMemory/
│   │       ├── Sqlite/
│   │       └── FileBased/
│   │
│   └── Configurations/
│       └── LlmConfigurator.cs         # Fluent builder API
│
├── LLMSharp.Console/                  # Demo application
│   └── Program.cs
│
├── LLMSharp.Tests/                    # Unit tests
│   └── Services/
│       └── Foundations/
│           └── Chats/
│               └── ChatServiceTests.cs
│
├── LLMSharp.sln
└── README.md
```

---

## Extending LLMSharp

### Creating a Custom Dataset Provider

```csharp
using LLMSharp.Models.Datasets;
using LLMSharp.Models.Trainings;

public class MyCustomProvider : IDatasetProvider
{
    public async ValueTask<IEnumerable<TrainingItem>> LoadTrainingDataAsync()
    {
        // Load from your custom source
        var items = new List<TrainingItem>();
        // ... load data ...
        return items;
    }

    public async ValueTask SaveTrainingResultAsync(TrainingResult result)
    {
        // Save to your custom destination
        // ... save logic ...
    }
}
```

### Creating a Custom Vector Store

```csharp
using LLMSharp.Models.VectorStores;

public class MyCustomVectorStore : IVectorStore
{
    public async ValueTask SaveVectorAsync(string key, float[] vector)
    {
        // Store vector with key
    }

    public async ValueTask<float[]> GetVectorAsync(string key)
    {
        // Retrieve vector by key
        return null;
    }

    public async ValueTask<IEnumerable<string>> SearchAsync(float[] vector, int topK)
    {
        // Perform similarity search
        return new List<string>();
    }
}
```

---

## Testing

LLMSharp is designed for testability. Every layer can be unit tested in isolation using mocks.

### Example Test

```csharp
[Fact]
public async Task ShouldGenerateChatResponseAsync()
{
    // given
    var modelEngineBrokerMock = new Mock<IModelEngineBroker>();
    var timeBrokerMock = new Mock<ITimeBroker>();

    var chatService = new ChatService(
        modelEngineBrokerMock.Object,
        timeBrokerMock.Object);

    var model = new ModelInstance { /* ... */ };
    var request = new ChatRequest { Prompt = "Hello" };

    modelEngineBrokerMock
        .Setup(b => b.GenerateTextAsync(model, "Hello", It.IsAny<GenerationOptions>()))
        .ReturnsAsync("Response");

    // when
    ChatResponse response = await chatService.ChatAsync(model, request);

    // then
    response.Content.Should().Be("Response");
}
```

### Run Tests

```bash
dotnet test
```

---

## Build Instructions

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Visual Studio 2022 / VS Code / Rider (optional)

### Clone and Build

```bash
# Clone the repository
git clone https://github.com/yourusername/LLMSharp.git
cd LLMSharp

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run tests
dotnet test

# Run the console demo
dotnet run --project LLMSharp.Console
```

### Build NuGet Package

```bash
dotnet pack LLMSharp/LLMSharp.csproj --configuration Release
```

The package will be in `LLMSharp/bin/Release/LLMSharp.1.0.0.nupkg`

---

## Architecture Principles

LLMSharp strictly adheres to **The Standard** by Hassan Habib:

1. **Brokers are the lowest level** - They wrap external dependencies
2. **Services contain business logic** - Foundation services are single-purpose
3. **Orchestration services coordinate** - They combine multiple services
4. **Clients provide user-facing APIs** - Clean, simple entry points
5. **Models are pure data** - No logic in models
6. **Everything is testable** - Use dependency injection and interfaces
7. **No hidden dependencies** - All dependencies explicit in constructors
8. **Async/ValueTask for efficiency** - Async all the way down

---

## Roadmap

- [ ] Streaming chat support (`StreamChatAsync`)
- [ ] Complete dataset provider implementations (SQL Server, SQLite, MongoDB, Filesystem)
- [ ] Complete vector store implementations
- [ ] Training orchestration pipeline
- [ ] Evaluation service
- [ ] Tokenization helpers
- [ ] Multi-turn conversation management
- [ ] Tool/function calling support
- [ ] Remote model loading (HTTP/URL)
- [ ] Performance benchmarks
- [ ] Integration with actual LLM libraries (LLamaSharp, OnnxRuntime)

---

## Contributing

Contributions are welcome! Please follow The Standard principles:

1. Fork the repository
2. Create a feature branch
3. Write tests for new functionality
4. Ensure all tests pass
5. Submit a pull request

---

## License

This project is licensed under the MIT License.

---

## Acknowledgments

- **Hassan Habib** for [The Standard](https://github.com/hassanhabib/The-Standard)
- Inspired by clean architecture and SOLID principles

---

## Contact

**Zafar Urakov**

For questions, issues, or feedback, please open an issue on GitHub.

---

**LLMSharp** - Industrial-grade LLM SDK for .NET, built with The Standard.
