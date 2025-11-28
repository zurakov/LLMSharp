// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using LLMSharp.Brokers.Loggings;
using LLMSharp.Brokers.ModelEngines;
using LLMSharp.Brokers.Monitorings;
using LLMSharp.Brokers.Times;
using LLMSharp.Brokers.VectorStores;
using LLMSharp.Clients;
using LLMSharp.Clients.Chats;
using LLMSharp.Clients.Vectors;
using LLMSharp.Models.Datasets;
using LLMSharp.Models.ModelEngines;
using LLMSharp.Models.VectorStores;
using LLMSharp.Providers.Datasets.InMemory;
using LLMSharp.Providers.VectorStores.InMemory;
using LLMSharp.Services.Foundations.Chats;
using LLMSharp.Services.Foundations.Embeddings;
using LLMSharp.Services.Foundations.ModelLoadings;
using LLMSharp.Services.Orchestrations.Chats;

namespace LLMSharp.Configurations
{
    public class LlmConfigurator
    {
        private string modelPath;
        private IDatasetProvider datasetProvider;
        private IVectorStore vectorStore;
        private bool monitoringEnabled;

        private LlmConfigurator() { }

        public static LlmConfigurator Configure() => new LlmConfigurator();

        public LlmConfigurator UseModel(string path)
        {
            this.modelPath = path;
            return this;
        }

        public LlmConfigurator UseDatasetProvider(IDatasetProvider provider)
        {
            this.datasetProvider = provider;
            return this;
        }

        public LlmConfigurator UseVectorStore(IVectorStore store)
        {
            this.vectorStore = store;
            return this;
        }

        public LlmConfigurator EnableMonitoring(bool enabled)
        {
            this.monitoringEnabled = enabled;
            return this;
        }

        public async ValueTask<LlmClient> BuildAsync()
        {
            this.datasetProvider ??= new InMemoryDatasetProvider();
            this.vectorStore ??= new InMemoryVectorStore();

            var loggingBroker = new LoggingBroker();
            var modelEngineBroker = new ModelEngineBroker();
            var monitoringBroker = new MonitoringBroker();
            var timeBroker = new TimeBroker();

            var modelLoadingService = new ModelLoadingService(
                modelEngineBroker,
                loggingBroker);

            ModelInstance model = await modelLoadingService.LoadModelAsync(this.modelPath);

            var chatService = new ChatService(modelEngineBroker, timeBroker);
            var embeddingService = new EmbeddingService(modelEngineBroker, timeBroker);

            var chatOrchestrationService = new ChatOrchestrationService(
                chatService,
                monitoringBroker,
                timeBroker,
                model);

            var vectorStoreBroker = new VectorStoreBroker(this.vectorStore);

            var chatClient = new ChatClient(chatOrchestrationService);
            var vectorClient = new VectorClient(embeddingService, vectorStoreBroker, model);

            return new LlmClient(chatClient, vectorClient);
        }
    }
}
