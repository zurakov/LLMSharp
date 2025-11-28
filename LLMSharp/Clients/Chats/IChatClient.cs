// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.Chats;

namespace LLMSharp.Clients.Chats
{
    public interface IChatClient
    {
        ValueTask<ChatResponse> ChatAsync(string prompt);
        ValueTask<ChatResponse> ChatAsync(ChatRequest request);
        IAsyncEnumerable<char> StreamChatAsync(string prompt);
        IAsyncEnumerable<char> StreamChatAsync(ChatRequest request);
    }
}
