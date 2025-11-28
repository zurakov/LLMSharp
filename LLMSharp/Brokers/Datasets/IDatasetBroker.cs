// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.Datasets;
using LLMSharp.Models.Trainings;

namespace LLMSharp.Brokers.Datasets
{
    public interface IDatasetBroker
    {
        ValueTask<IEnumerable<TrainingItem>> LoadTrainingDataAsync();
        ValueTask SaveTrainingResultAsync(TrainingResult result);
    }
}
