// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLama;
using LLama.Common;
using LLama.Sampling;
using LLMSharp.Models.ModelEngines;

namespace LLMSharp.Brokers.ModelEngines
{
    public class ModelEngineBroker : IModelEngineBroker
    {
        public async ValueTask<ModelInstance> LoadModelAsync(string modelPath)
        {
            var modelParams = new ModelParams(modelPath)
            {
                ContextSize = 4096,
                GpuLayerCount = 0
            };

            var weights = await Task.Run(() => LLamaWeights.LoadFromFile(modelParams));
            var context = await Task.Run(() => weights.CreateContext(modelParams));

            return new ModelInstance
            {
                Id = Guid.NewGuid(),
                ModelPath = modelPath,
                IsLoaded = true,
                LoadedAt = DateTimeOffset.UtcNow,
                Weights = weights,
                Context = context,
                ModelParams = modelParams
            };
        }

        public async ValueTask<string> GenerateTextAsync(
            ModelInstance model,
            string prompt,
            GenerationOptions options)
        {
            var executor = new InteractiveExecutor(model.Context);

            var samplingPipeline = new DefaultSamplingPipeline
            {
                Temperature = (float)options.Temperature,
                TopP = (float)options.TopP
            };

            var inferenceParams = new InferenceParams
            {
                MaxTokens = options.MaxTokens,
                SamplingPipeline = samplingPipeline,
                AntiPrompts = options.StopSequences?.ToList() ?? new List<string>()
            };

            var response = new StringBuilder();

            await foreach (var token in executor.InferAsync(prompt, inferenceParams))
            {
                response.Append(token);
            }

            return response.ToString();
        }

        public async IAsyncEnumerable<char> StreamGenerateTextAsync(
            ModelInstance model,
            string prompt,
            GenerationOptions options)
        {
            var executor = new InteractiveExecutor(model.Context);

            var samplingPipeline = new DefaultSamplingPipeline
            {
                Temperature = (float)options.Temperature,
                TopP = (float)options.TopP
            };

            var inferenceParams = new InferenceParams
            {
                MaxTokens = options.MaxTokens,
                SamplingPipeline = samplingPipeline,
                AntiPrompts = options.StopSequences?.ToList() ?? new List<string>()
            };

            await foreach (var token in executor.InferAsync(prompt, inferenceParams))
            {
                foreach (char c in token)
                {
                    yield return c;
                }
            }
        }

        public async ValueTask<float[]> GenerateEmbeddingAsync(ModelInstance model, string text)
        {
            var embedder = new LLamaEmbedder(model.Weights, model.ModelParams);
            var embeddingsList = await Task.Run(() => embedder.GetEmbeddings(text));
            return embeddingsList.FirstOrDefault() ?? Array.Empty<float>();
        }

        public async ValueTask UnloadModelAsync(ModelInstance model)
        {
            await Task.Run(() =>
            {
                model.Context?.Dispose();
                model.Weights?.Dispose();
                model.IsLoaded = false;
            });
        }

        public async ValueTask<int> GetTokenCountAsync(ModelInstance model, string text)
        {
            var tokens = await Task.Run(() => model.Context.Tokenize(text));
            return tokens.Length;
        }
    }
}
