// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Brokers.Monitorings;
using LLMSharp.Brokers.Times;
using LLMSharp.Models.Chats;
using LLMSharp.Models.ModelEngines;
using LLMSharp.Models.Monitorings;
using LLMSharp.Services.Foundations.Chats;

namespace LLMSharp.Services.Orchestrations.Chats
{
    public class ChatOrchestrationService : IChatOrchestrationService
    {
        private readonly IChatService chatService;
        private readonly IMonitoringBroker monitoringBroker;
        private readonly ITimeBroker timeBroker;
        private readonly ModelInstance currentModel;

        public ChatOrchestrationService(
            IChatService chatService,
            IMonitoringBroker monitoringBroker,
            ITimeBroker timeBroker,
            ModelInstance currentModel)
        {
            this.chatService = chatService;
            this.monitoringBroker = monitoringBroker;
            this.timeBroker = timeBroker;
            this.currentModel = currentModel;
        }

        public async ValueTask<ChatResponse> ProcessChatAsync(ChatRequest request)
        {
            var startTime = this.timeBroker.GetCurrentDateTimeOffset();

            ChatResponse response = await this.chatService.ChatAsync(
                this.currentModel,
                request);

            var endTime = this.timeBroker.GetCurrentDateTimeOffset();

            await this.monitoringBroker.LogInteractionEventAsync(new InteractionEvent
            {
                Timestamp = endTime,
                Type = InteractionType.Chat,
                Duration = endTime - startTime,
                TokenCount = response.TokenCount,
                ModelPath = response.ModelPath,
                Prompt = request.Prompt
            });

            return response;
        }

        public async IAsyncEnumerable<char> StreamChatAsync(ChatRequest request)
        {
            await foreach (char c in this.chatService.StreamChatAsync(this.currentModel, request))
            {
                yield return c;
            }
        }
    }
}
