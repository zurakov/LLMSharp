// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Brokers.ModelEngines;
using LLMSharp.Brokers.Times;
using LLMSharp.Models.Chats;
using LLMSharp.Models.ModelEngines;

namespace LLMSharp.Services.Foundations.Chats
{
    public class ChatService : IChatService
    {
        private readonly IModelEngineBroker modelEngineBroker;
        private readonly ITimeBroker timeBroker;

        public ChatService(
            IModelEngineBroker modelEngineBroker,
            ITimeBroker timeBroker)
        {
            this.modelEngineBroker = modelEngineBroker;
            this.timeBroker = timeBroker;
        }

        public async ValueTask<ChatResponse> ChatAsync(ModelInstance model, ChatRequest request)
        {
            DateTimeOffset startTime = this.timeBroker.GetCurrentDateTimeOffset();

            var options = new GenerationOptions
            {
                MaxTokens = request.MaxTokens,
                Temperature = request.Temperature,
                TopP = request.TopP,
                StopSequences = request.StopSequences
            };

            string content = await this.modelEngineBroker.GenerateTextAsync(
                model,
                request.Prompt,
                options);

            int tokenCount = await this.modelEngineBroker.GetTokenCountAsync(model, content);
            DateTimeOffset endTime = this.timeBroker.GetCurrentDateTimeOffset();

            return new ChatResponse
            {
                Content = content,
                TokenCount = tokenCount,
                Duration = endTime - startTime,
                Timestamp = endTime,
                ModelPath = model.ModelPath
            };
        }

        public async IAsyncEnumerable<char> StreamChatAsync(ModelInstance model, ChatRequest request)
        {
            var options = new GenerationOptions
            {
                MaxTokens = request.MaxTokens,
                Temperature = request.Temperature,
                TopP = request.TopP,
                StopSequences = request.StopSequences
            };

            await foreach (char c in this.modelEngineBroker.StreamGenerateTextAsync(
                model,
                request.Prompt,
                options))
            {
                yield return c;
            }
        }
    }
}
