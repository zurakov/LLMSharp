// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using LLMSharp.Models.Embeddings;
using LLMSharp.Models.VectorStores;

namespace LLMSharp.Brokers.VectorStores
{
    public class VectorStoreBroker : IVectorStoreBroker
    {
        private readonly IVectorStore vectorStore;

        public VectorStoreBroker(IVectorStore vectorStore)
        {
            this.vectorStore = vectorStore;
        }

        public async Task SaveVectorAsync(string key, float[] vector)
        {
            await this.vectorStore.SaveVectorAsync(key, vector);
        }

        public async Task<float[]> GetVectorAsync(string key)
        {
            return await this.vectorStore.GetVectorAsync(key);
        }

        public async Task<IEnumerable<string>> SearchAsync(float[] vector, int topK)
        {
            return await this.vectorStore.SearchAsync(vector, topK);
        }

        public async Task SaveVectorEmbeddingAsync(VectorEmbedding embedding)
        {
            await this.vectorStore.SaveVectorAsync(embedding.Key, embedding.Vector);
        }
    }
}
