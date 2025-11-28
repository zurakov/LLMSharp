// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using LLama;
using LLama.Common;

namespace LLMSharp.Models.ModelEngines
{
    public class ModelInstance
    {
        public Guid Id { get; set; }
        public string ModelPath { get; set; }
        public bool IsLoaded { get; set; }
        public DateTimeOffset LoadedAt { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
        public LLamaWeights Weights { get; set; }
        public LLamaContext Context { get; set; }
        public ModelParams ModelParams { get; set; }
    }
}
