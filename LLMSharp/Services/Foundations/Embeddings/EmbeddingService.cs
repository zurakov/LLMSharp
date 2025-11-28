// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using LLMSharp.Brokers.ModelEngines;
using LLMSharp.Brokers.Times;
using LLMSharp.Models.Embeddings;
using LLMSharp.Models.ModelEngines;

namespace LLMSharp.Services.Foundations.Embeddings
{
    public class EmbeddingService : IEmbeddingService
    {
        private readonly IModelEngineBroker modelEngineBroker;
        private readonly ITimeBroker timeBroker;

        public EmbeddingService(
            IModelEngineBroker modelEngineBroker,
            ITimeBroker timeBroker)
        {
            this.modelEngineBroker = modelEngineBroker;
            this.timeBroker = timeBroker;
        }

        public async ValueTask<EmbeddingResult> CreateEmbeddingAsync(
            ModelInstance model,
            string text)
        {
            DateTimeOffset startTime = this.timeBroker.GetCurrentDateTimeOffset();

            float[] vector = await this.modelEngineBroker.GenerateEmbeddingAsync(model, text);

            DateTimeOffset endTime = this.timeBroker.GetCurrentDateTimeOffset();

            return new EmbeddingResult
            {
                Vector = vector,
                Text = text,
                Dimensions = vector.Length,
                Timestamp = endTime,
                Duration = endTime - startTime
            };
        }
    }
}
