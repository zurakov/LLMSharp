// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Streamings
{
    public class StreamingEvent
    {
        public char Token { get; set; }
        public int Position { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public StreamingEventType Type { get; set; }
    }
}
