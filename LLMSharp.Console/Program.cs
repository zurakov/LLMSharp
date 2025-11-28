// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using LLMSharp.Clients;
using LLMSharp.Configurations;
using LLMSharp.Models.Chats;

namespace LLMSharp.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("===========================================");
            System.Console.WriteLine("       LLMSharp - Demo Application");
            System.Console.WriteLine("===========================================");
            System.Console.WriteLine();

            System.Console.WriteLine("Initializing LLMSharp...");

            string modelPath = @"";
            System.Console.WriteLine($"Loading model: {modelPath}");
            System.Console.WriteLine();

            LlmClient llm = await LlmConfigurator
                .Configure()
                .UseModel(modelPath)
                .EnableMonitoring(true)
                .BuildAsync();

            System.Console.WriteLine("Model loaded successfully!");
            System.Console.WriteLine();

            System.Console.WriteLine("===========================================");
            System.Console.WriteLine("          Chat Demo (Streaming)");
            System.Console.WriteLine("===========================================");

            string prompt = "Hello! How are you today?";
            System.Console.WriteLine($"Prompt: {prompt}");
            System.Console.WriteLine();
            System.Console.WriteLine("Response:");

            await foreach (char c in llm.Chat.StreamChatAsync(prompt))
            {
                System.Console.Write(c);
            }

            System.Console.WriteLine();
            System.Console.WriteLine();

            System.Console.WriteLine("===========================================");
            System.Console.WriteLine("  Advanced Chat Request Demo (Streaming)");
            System.Console.WriteLine("===========================================");

            var advancedRequest = new ChatRequest
            {
                Prompt = "What is the wheather like today?",
                MaxTokens = 100,
                Temperature = 0.8,
                TopP = 0.95
            };

            System.Console.WriteLine($"Prompt: {advancedRequest.Prompt}");
            System.Console.WriteLine($"Settings: MaxTokens={advancedRequest.MaxTokens}, Temperature={advancedRequest.Temperature}");
            System.Console.WriteLine();
            System.Console.WriteLine("Response:");

            await foreach (char c in llm.Chat.StreamChatAsync(advancedRequest))
            {
                System.Console.Write(c);
            }

            System.Console.WriteLine();
            System.Console.WriteLine();

            System.Console.WriteLine("===========================================");
            System.Console.WriteLine("      Vector/Embedding Demo");
            System.Console.WriteLine("===========================================");

            System.Console.WriteLine("Creating embeddings and storing vectors...");
            await llm.Vectors.SaveEmbeddingAsync("doc1", "Artificial intelligence is fascinating");
            await llm.Vectors.SaveEmbeddingAsync("doc2", "Machine learning is a subset of AI");
            await llm.Vectors.SaveEmbeddingAsync("doc3", "The weather is nice today");

            System.Console.WriteLine("Searching for similar documents...");
            var similar = await llm.Vectors.SearchSimilarAsync("AI and ML concepts", 2);

            System.Console.WriteLine("Similar documents:");
            foreach (var key in similar)
            {
                System.Console.WriteLine($"  - {key}");
            }

            System.Console.WriteLine();
            System.Console.WriteLine("===========================================");
            System.Console.WriteLine("Demo completed successfully!");
            System.Console.WriteLine("===========================================");
        }
    }
}
