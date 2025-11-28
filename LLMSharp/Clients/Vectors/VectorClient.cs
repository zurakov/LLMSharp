// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Brokers.VectorStores;
using LLMSharp.Models.Embeddings;
using LLMSharp.Models.ModelEngines;
using LLMSharp.Services.Foundations.Embeddings;

namespace LLMSharp.Clients.Vectors
{
    public class VectorClient : IVectorClient
    {
        private readonly IEmbeddingService embeddingService;
        private readonly IVectorStoreBroker vectorStoreBroker;
        private readonly ModelInstance model;

        public VectorClient(
            IEmbeddingService embeddingService,
            IVectorStoreBroker vectorStoreBroker,
            ModelInstance model)
        {
            this.embeddingService = embeddingService;
            this.vectorStoreBroker = vectorStoreBroker;
            this.model = model;
        }

        public async ValueTask<EmbeddingResult> CreateEmbeddingAsync(string text) =>
            await this.embeddingService.CreateEmbeddingAsync(this.model, text);

        public async ValueTask SaveEmbeddingAsync(string key, string text)
        {
            EmbeddingResult embedding = await this.embeddingService.CreateEmbeddingAsync(
                this.model,
                text);

            await this.vectorStoreBroker.SaveVectorAsync(key, embedding.Vector);
        }

        public async ValueTask<IEnumerable<string>> SearchSimilarAsync(string queryText, int topK)
        {
            EmbeddingResult queryEmbedding = await this.embeddingService.CreateEmbeddingAsync(
                this.model,
                queryText);

            return await this.vectorStoreBroker.SearchAsync(queryEmbedding.Vector, topK);
        }
    }
}
