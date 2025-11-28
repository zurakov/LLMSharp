// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using LLMSharp.Clients.Chats;
using LLMSharp.Clients.Vectors;

namespace LLMSharp.Clients
{
    public class LlmClient
    {
        public IChatClient Chat { get; }
        public IVectorClient Vectors { get; }

        public LlmClient(
            IChatClient chatClient,
            IVectorClient vectorClient)
        {
            this.Chat = chatClient;
            this.Vectors = vectorClient;
        }
    }
}
