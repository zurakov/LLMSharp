// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace LLMSharp.Models.Trainings
{
    public class TrainingDataset
    {
        public string Name { get; set; }
        public List<TrainingItem> Items { get; set; } = new();
        public DateTimeOffset CreatedAt { get; set; }
        public int TotalItems => Items.Count;
    }
}
