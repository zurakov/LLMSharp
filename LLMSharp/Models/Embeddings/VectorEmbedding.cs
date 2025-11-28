// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Embeddings
{
    public class VectorEmbedding
    {
        public string Key { get; set; }
        public float[] Vector { get; set; }
        public string Metadata { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
