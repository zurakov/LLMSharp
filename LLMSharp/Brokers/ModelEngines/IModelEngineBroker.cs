// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using LLMSharp.Models.ModelEngines;

namespace LLMSharp.Brokers.ModelEngines
{
    public interface IModelEngineBroker
    {
        Task<ModelInstance> LoadModelAsync(string modelPath);
        Task<string> GenerateTextAsync(ModelInstance model, string prompt, GenerationOptions options);
        IAsyncEnumerable<char> StreamGenerateTextAsync(ModelInstance model, string prompt, GenerationOptions options);
        Task<float[]> GenerateEmbeddingAsync(ModelInstance model, string text);
        Task UnloadModelAsync(ModelInstance model);
        Task<int> GetTokenCountAsync(ModelInstance model, string text);
    }
}
