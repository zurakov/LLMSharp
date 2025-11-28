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
        private string FormatPromptForInstruct(string prompt) => $"[INST] {prompt} [/INST]";

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
            string formattedPrompt = FormatPromptForInstruct(prompt);

            await foreach (var token in executor.InferAsync(formattedPrompt, inferenceParams))
            {
                response.Append(token);
            }

            string result = response.ToString();

            if (options.StopSequences != null)
            {
                foreach (var stopSeq in options.StopSequences)
                {
                    if (result.EndsWith(stopSeq))
                    {
                        result = result.Substring(0, result.Length - stopSeq.Length);
                        break;
                    }
                }
            }

            return result.TrimEnd();
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

            string formattedPrompt = FormatPromptForInstruct(prompt);

            // Calculate the maximum stop sequence length for buffering
            int maxStopSeqLength = 0;
            if (options.StopSequences != null)
            {
                foreach (var seq in options.StopSequences)
                {
                    if (seq.Length > maxStopSeqLength)
                        maxStopSeqLength = seq.Length;
                }
            }

            var buffer = new StringBuilder();

            await foreach (var token in executor.InferAsync(formattedPrompt, inferenceParams))
            {
                buffer.Append(token);

                while (buffer.Length > maxStopSeqLength)
                {
                    yield return buffer[0];
                    buffer.Remove(0, 1);
                }
            }

            string remaining = buffer.ToString();

            if (options.StopSequences != null)
            {
                foreach (var stopSeq in options.StopSequences)
                {
                    if (remaining.EndsWith(stopSeq))
                    {
                        remaining = remaining.Substring(0, remaining.Length - stopSeq.Length);
                        break;
                    }
                }
            }

            remaining = remaining.TrimEnd();

            foreach (char c in remaining)
            {
                yield return c;
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
