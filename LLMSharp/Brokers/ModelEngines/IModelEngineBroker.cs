// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.ModelEngines;

namespace LLMSharp.Brokers.ModelEngines
{
    public interface IModelEngineBroker
    {
        ValueTask<ModelInstance> LoadModelAsync(string modelPath);
        ValueTask<string> GenerateTextAsync(ModelInstance model, string prompt, GenerationOptions options);
        IAsyncEnumerable<char> StreamGenerateTextAsync(ModelInstance model, string prompt, GenerationOptions options);
        ValueTask<float[]> GenerateEmbeddingAsync(ModelInstance model, string text);
        ValueTask UnloadModelAsync(ModelInstance model);
        ValueTask<int> GetTokenCountAsync(ModelInstance model, string text);
    }
}
