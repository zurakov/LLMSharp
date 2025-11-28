// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async ValueTask SaveVectorAsync(string key, float[] vector) =>
            await this.vectorStore.SaveVectorAsync(key, vector);

        public async ValueTask<float[]> GetVectorAsync(string key) =>
            await this.vectorStore.GetVectorAsync(key);

        public async ValueTask<IEnumerable<string>> SearchAsync(float[] vector, int topK) =>
            await this.vectorStore.SearchAsync(vector, topK);

        public async ValueTask SaveVectorEmbeddingAsync(VectorEmbedding embedding) =>
            await this.vectorStore.SaveVectorAsync(embedding.Key, embedding.Vector);
    }
}
