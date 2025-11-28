// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.Datasets;
using LLMSharp.Models.Trainings;

namespace LLMSharp.Providers.Datasets.InMemory
{
    public class InMemoryDatasetProvider : IDatasetProvider
    {
        private readonly List<TrainingItem> trainingItems = new();
        private readonly List<TrainingResult> trainingResults = new();

#pragma warning disable CS1998
        public async ValueTask<IEnumerable<TrainingItem>> LoadTrainingDataAsync() =>
#pragma warning restore CS1998
            this.trainingItems;

#pragma warning disable CS1998
        public async ValueTask SaveTrainingResultAsync(TrainingResult result) =>
#pragma warning restore CS1998
            this.trainingResults.Add(result);
    }
}
