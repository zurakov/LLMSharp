// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.Trainings;

namespace LLMSharp.Models.Datasets
{
    public interface IDatasetProvider
    {
        ValueTask<IEnumerable<TrainingItem>> LoadTrainingDataAsync();
        ValueTask SaveTrainingResultAsync(TrainingResult result);
    }
}
