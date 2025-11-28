// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Embeddings
{
    public class EmbeddingResult
    {
        public float[] Vector { get; set; }
        public string Text { get; set; }
        public int Dimensions { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
