// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

namespace LLMSharp.Models.ModelEngines
{
    public class GenerationOptions
    {
        public int MaxTokens { get; set; } = 2048;
        public double Temperature { get; set; } = 0.7;
        public double TopP { get; set; } = 0.9;
        public double FrequencyPenalty { get; set; } = 0.0;
        public double PresencePenalty { get; set; } = 0.0;
        public string[] StopSequences { get; set; } = new[] { "[INST]", "\n\nUser:", "\n\nHuman:" };
    }
}
