// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Monitorings
{
    public class TokenStats
    {
        public int TotalTokens { get; set; }
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
