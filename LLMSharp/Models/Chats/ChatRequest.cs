// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace LLMSharp.Models.Chats
{
    public class ChatRequest
    {
        public string Prompt { get; set; }
        public List<ChatMessage> Messages { get; set; } = new();
        public int MaxTokens { get; set; } = 2048;
        public double Temperature { get; set; } = 0.7;
        public double TopP { get; set; } = 0.9;
        public string SystemMessage { get; set; }
        public string[] StopSequences { get; set; } = new[] { "[INST]", "\n\nUser:", "\n\nHuman:" };
    }
}
