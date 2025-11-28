# LLMSharp Quick Start Guide

Get up and running with LLMSharp in 5 minutes!

---

## Installation

### Option 1: From NuGet (when published)

```bash
dotnet add package LLMSharp
```

### Option 2: Build from Source

```bash
git clone https://github.com/yourusername/LLMSharp.git
cd LLMSharp
dotnet build
```

---

## Basic Usage

### 1. Configure and Initialize

```csharp
using LLMSharp.Clients;
using LLMSharp.Configurations;

// Configure the LLM client
LlmClient llm = await LlmConfigurator
    .Configure()
    .UseModel("path/to/your/model.gguf")
    .EnableMonitoring(true)
    .BuildAsync();
```

### 2. Chat with the Model

```csharp
using LLMSharp.Models.Chats;

// Simple chat
ChatResponse response = await llm.Chat.ChatAsync("Hello, how are you?");
Console.WriteLine(response.Content);
Console.WriteLine($"Tokens: {response.TokenCount}");
Console.WriteLine($"Duration: {response.Duration.TotalMilliseconds}ms");
```

### 3. Advanced Chat with Options

```csharp
var request = new ChatRequest
{
    Prompt = "Explain quantum computing in simple terms",
    MaxTokens = 500,
    Temperature = 0.8,
    TopP = 0.95,
    SystemMessage = "You are a helpful science tutor."
};

ChatResponse response = await llm.Chat.ChatAsync(request);
Console.WriteLine(response.Content);
```

### 4. Create Embeddings

```csharp
using LLMSharp.Models.Embeddings;

// Create a single embedding
EmbeddingResult embedding = await llm.Vectors.CreateEmbeddingAsync(
    "Machine learning is a subset of artificial intelligence"
);

Console.WriteLine($"Vector dimensions: {embedding.Dimensions}");
Console.WriteLine($"Vector created in: {embedding.Duration.TotalMilliseconds}ms");
```

### 5. Vector Similarity Search

```csharp
// Store documents as vectors
await llm.Vectors.SaveEmbeddingAsync("doc1", "AI is transforming technology");
await llm.Vectors.SaveEmbeddingAsync("doc2", "Machine learning powers modern apps");
await llm.Vectors.SaveEmbeddingAsync("doc3", "The weather forecast for tomorrow");

// Search for similar documents
IEnumerable<string> similar = await llm.Vectors.SearchSimilarAsync(
    queryText: "Tell me about artificial intelligence",
    topK: 2
);

foreach (var docKey in similar)
{
    Console.WriteLine($"Similar document: {docKey}");
}
```

---

## Complete Example

Here's a complete console application:

```csharp
using System;
using System.Threading.Tasks;
using LLMSharp.Clients;
using LLMSharp.Configurations;
using LLMSharp.Models.Chats;

namespace MyLlmApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 1. Configure and build the client
            LlmClient llm = await LlmConfigurator
                .Configure()
                .UseModel("path/to/model.gguf")
                .EnableMonitoring(true)
                .BuildAsync();

            Console.WriteLine("LLMSharp initialized successfully!");

            // 2. Chat
            var response = await llm.Chat.ChatAsync("What is the capital of France?");
            Console.WriteLine($"AI: {response.Content}");
            Console.WriteLine($"Used {response.TokenCount} tokens in {response.Duration.TotalSeconds:F2}s");

            // 3. Embeddings
            await llm.Vectors.SaveEmbeddingAsync("fact1", "Paris is the capital of France");
            await llm.Vectors.SaveEmbeddingAsync("fact2", "Berlin is the capital of Germany");

            var similar = await llm.Vectors.SearchSimilarAsync("European capitals", 1);
            Console.WriteLine($"Most similar document: {string.Join(", ", similar)}");
        }
    }
}
```

---

## Configuration Options

### Use Custom Dataset Provider

```csharp
using LLMSharp.Models.Datasets;
using LLMSharp.Providers.Datasets.SqlServer;

IDatasetProvider provider = new SqlLocalDbProvider(
    "Server=(localdb)\\MSSQLLocalDB;Database=MyData;");

LlmClient llm = await LlmConfigurator
    .Configure()
    .UseModel("model.gguf")
    .UseDatasetProvider(provider)
    .BuildAsync();
```

### Use Custom Vector Store

```csharp
using LLMSharp.Models.VectorStores;
using LLMSharp.Providers.VectorStores.Sqlite;

IVectorStore store = new SqliteVectorStore("vectors.db");

LlmClient llm = await LlmConfigurator
    .Configure()
    .UseModel("model.gguf")
    .UseVectorStore(store)
    .BuildAsync();
```

---

## Running the Demo

### Option 1: Run the Console Application

```bash
cd LLMSharp.Console
dotnet run
```

### Option 2: Run the Tests

```bash
cd LLMSharp.Tests
dotnet test
```

---

## Troubleshooting

### Issue: Model file not found

**Error**: `FileNotFoundException: Could not find file 'model.gguf'`

**Solution**: Ensure you provide the **absolute path** to your GGUF model file:

```csharp
string modelPath = @"C:\Models\mistral-7b.gguf";
.UseModel(modelPath)
```

### Issue: NuGet packages not restored

**Error**: Build errors about missing packages

**Solution**: Restore NuGet packages:

```bash
dotnet restore
```

### Issue: Tests fail

**Error**: Test failures

**Solution**: Ensure you've built the solution first:

```bash
dotnet build
dotnet test
```

---

## Next Steps

1. **Explore the Architecture**: Read [ARCHITECTURE.md](ARCHITECTURE.md)
2. **Read the Full Documentation**: See [README.md](README.md)
3. **Create Custom Providers**: Implement `IDatasetProvider` or `IVectorStore`
4. **Integrate Real LLMs**: Replace `ModelEngineBroker` with LLamaSharp or OnnxRuntime
5. **Add Streaming**: Implement streaming chat support
6. **Build Production Apps**: Use LLMSharp in your real applications!

---

## Support

- **Issues**: https://github.com/yourusername/LLMSharp/issues
- **Discussions**: https://github.com/yourusername/LLMSharp/discussions

---

**Happy Coding with LLMSharp!**

Designed by Zafar Urakov | Following The Standard by Hassan Habib
