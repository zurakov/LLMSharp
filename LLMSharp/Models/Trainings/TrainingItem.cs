// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace LLMSharp.Models.Trainings
{
    public class TrainingItem
    {
        public string Id { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
    }
}
