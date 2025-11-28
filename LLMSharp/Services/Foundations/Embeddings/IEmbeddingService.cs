// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using LLMSharp.Models.Embeddings;
using LLMSharp.Models.ModelEngines;

namespace LLMSharp.Services.Foundations.Embeddings
{
    public interface IEmbeddingService
    {
        ValueTask<EmbeddingResult> CreateEmbeddingAsync(ModelInstance model, string text);
    }
}
