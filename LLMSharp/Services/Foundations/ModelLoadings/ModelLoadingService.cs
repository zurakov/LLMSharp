// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using LLMSharp.Brokers.Loggings;
using LLMSharp.Brokers.ModelEngines;
using LLMSharp.Models.ModelEngines;

namespace LLMSharp.Services.Foundations.ModelLoadings
{
    public class ModelLoadingService : IModelLoadingService
    {
        private readonly IModelEngineBroker modelEngineBroker;
        private readonly ILoggingBroker loggingBroker;

        public ModelLoadingService(
            IModelEngineBroker modelEngineBroker,
            ILoggingBroker loggingBroker)
        {
            this.modelEngineBroker = modelEngineBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<ModelInstance> LoadModelAsync(string modelPath)
        {
            this.loggingBroker.LogInformation($"Loading model from: {modelPath}");
            ModelInstance model = await this.modelEngineBroker.LoadModelAsync(modelPath);
            this.loggingBroker.LogInformation($"Model loaded successfully: {model.Id}");

            return model;
        }

        public async ValueTask UnloadModelAsync(ModelInstance model)
        {
            this.loggingBroker.LogInformation($"Unloading model: {model.Id}");
            await this.modelEngineBroker.UnloadModelAsync(model);
            this.loggingBroker.LogInformation("Model unloaded successfully");
        }
    }
}
