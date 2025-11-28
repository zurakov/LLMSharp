// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.Chats;
using LLMSharp.Models.ModelEngines;

namespace LLMSharp.Services.Foundations.Chats
{
    public interface IChatService
    {
        ValueTask<ChatResponse> ChatAsync(ModelInstance model, ChatRequest request);
        IAsyncEnumerable<char> StreamChatAsync(ModelInstance model, ChatRequest request);
    }
}
