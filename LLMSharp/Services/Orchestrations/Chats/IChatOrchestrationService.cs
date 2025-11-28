// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.Chats;

namespace LLMSharp.Services.Orchestrations.Chats
{
    public interface IChatOrchestrationService
    {
        ValueTask<ChatResponse> ProcessChatAsync(ChatRequest request);
        IAsyncEnumerable<char> StreamChatAsync(ChatRequest request);
    }
}
