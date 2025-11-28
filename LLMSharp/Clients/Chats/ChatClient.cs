// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.Chats;
using LLMSharp.Services.Orchestrations.Chats;

namespace LLMSharp.Clients.Chats
{
    public class ChatClient : IChatClient
    {
        private readonly IChatOrchestrationService chatOrchestrationService;

        public ChatClient(IChatOrchestrationService chatOrchestrationService)
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

        public async IAsyncEnumerable<char> StreamChatAsync(string prompt)
        {
            var request = new ChatRequest { Prompt = prompt };
            await foreach (char c in this.chatOrchestrationService.StreamChatAsync(request))
            {
                yield return c;
            }
        }

        public async IAsyncEnumerable<char> StreamChatAsync(ChatRequest request)
        {
            await foreach (char c in this.chatOrchestrationService.StreamChatAsync(request))
            {
                yield return c;
            }
        }
    }
}
