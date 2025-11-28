// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

namespace LLMSharp.Models.Trainings
{
    public class TrainingOptions
    {
        public int Epochs { get; set; } = 3;
        public double LearningRate { get; set; } = 0.001;
        public int BatchSize { get; set; } = 32;
        public string OutputPath { get; set; }
        public bool SaveCheckpoints { get; set; } = true;
        public int CheckpointFrequency { get; set; } = 1000;
    }
}
