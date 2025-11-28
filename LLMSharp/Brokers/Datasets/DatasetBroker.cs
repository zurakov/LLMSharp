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
    public class DatasetBroker : IDatasetBroker
    {
        private readonly IDatasetProvider datasetProvider;

        public DatasetBroker(IDatasetProvider datasetProvider)
        {
            this.datasetProvider = datasetProvider;
        }

        public async ValueTask<IEnumerable<TrainingItem>> LoadTrainingDataAsync() =>
            await this.datasetProvider.LoadTrainingDataAsync();

        public async ValueTask SaveTrainingResultAsync(TrainingResult result) =>
            await this.datasetProvider.SaveTrainingResultAsync(result);
    }
}
