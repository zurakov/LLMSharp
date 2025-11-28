// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Chats
{
    public class ChatResponse
    {
        public string Content { get; set; }
        public int TokenCount { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ModelPath { get; set; }
    }
}
