// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using LLMSharp.Models.Datasets;
using LLMSharp.Models.Trainings;

namespace LLMSharp.Brokers.Datasets
{
    public interface IDatasetBroker
    {
        Task<IEnumerable<TrainingItem>> LoadTrainingDataAsync();
        Task SaveTrainingResultAsync(TrainingResult result);
    }
}
