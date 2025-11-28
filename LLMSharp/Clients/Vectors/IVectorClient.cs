// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.Embeddings;

namespace LLMSharp.Clients.Vectors
{
    public interface IVectorClient
    {
        ValueTask<EmbeddingResult> CreateEmbeddingAsync(string text);
        ValueTask SaveEmbeddingAsync(string key, string text);
        ValueTask<IEnumerable<string>> SearchSimilarAsync(string queryText, int topK);
    }
}
