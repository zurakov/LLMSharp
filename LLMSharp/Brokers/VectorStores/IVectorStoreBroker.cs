// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using LLMSharp.Models.Embeddings;

namespace LLMSharp.Brokers.VectorStores
{
    public interface IVectorStoreBroker
    {
        Task SaveVectorAsync(string key, float[] vector);
        Task<float[]> GetVectorAsync(string key);
        Task<IEnumerable<string>> SearchAsync(float[] vector, int topK);
        Task SaveVectorEmbeddingAsync(VectorEmbedding embedding);
    }
}
