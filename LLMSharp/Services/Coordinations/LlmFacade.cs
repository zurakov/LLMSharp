// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using LLMSharp.Models.Chats;
using LLMSharp.Services.Orchestrations.Chats;

namespace LLMSharp.Services.Coordinations
{
    public class LlmFacade
    {
        private readonly IChatOrchestrationService chatOrchestrationService;

        public LlmFacade(IChatOrchestrationService chatOrchestrationService)
        {
            this.chatOrchestrationService = chatOrchestrationService;
        }

        public async ValueTask<ChatResponse> ChatAsync(string prompt)
        {
            var request = new ChatRequest { Prompt = prompt };
            return await this.chatOrchestrationService.ProcessChatAsync(request);
        }

        public async ValueTask<ChatResponse> ChatAsync(ChatRequest request) =>
            await this.chatOrchestrationService.ProcessChatAsync(request);
    }
}
