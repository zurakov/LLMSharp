// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

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

        public async Task<IEnumerable<TrainingItem>> LoadTrainingDataAsync()
        {
            return await this.datasetProvider.LoadTrainingDataAsync();
        }

        public async Task SaveTrainingResultAsync(TrainingResult result)
        {
            await this.datasetProvider.SaveTrainingResultAsync(result);
        }
    }
}
