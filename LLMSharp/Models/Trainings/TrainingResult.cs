// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace LLMSharp.Models.Trainings
{
    public class TrainingResult
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public double Loss { get; set; }
        public double Accuracy { get; set; }
        public int EpochsCompleted { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTimeOffset StartedAt { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public string ModelOutputPath { get; set; }
        public Dictionary<string, object> Metrics { get; set; } = new();
    }
}
