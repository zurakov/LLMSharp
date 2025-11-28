// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Chats
{
    public class ChatMessage
    {
        public MessageRole Role { get; set; }
        public string Content { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
