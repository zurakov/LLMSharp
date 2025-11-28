// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace LLMSharp.Models.Monitorings
{
    public class InteractionEvent
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset Timestamp { get; set; }
        public InteractionType Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int? TokenCount { get; set; }
        public string ModelPath { get; set; }
        public string Prompt { get; set; }
        public Dictionary<string, object> AdditionalMetadata { get; set; } = new();
    }
}
