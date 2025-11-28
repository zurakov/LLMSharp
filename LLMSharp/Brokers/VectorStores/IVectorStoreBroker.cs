// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.Embeddings;

namespace LLMSharp.Brokers.VectorStores
{
    public interface IVectorStoreBroker
    {
        ValueTask SaveVectorAsync(string key, float[] vector);
        ValueTask<float[]> GetVectorAsync(string key);
        ValueTask<IEnumerable<string>> SearchAsync(float[] vector, int topK);
        ValueTask SaveVectorEmbeddingAsync(VectorEmbedding embedding);
    }
}
