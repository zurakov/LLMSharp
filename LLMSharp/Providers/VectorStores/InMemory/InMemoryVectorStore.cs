// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LLMSharp.Models.VectorStores;

namespace LLMSharp.Providers.VectorStores.InMemory
{
    public class InMemoryVectorStore : IVectorStore
    {
        private readonly Dictionary<string, float[]> vectors = new();

#pragma warning disable CS1998
        public async ValueTask SaveVectorAsync(string key, float[] vector) =>
#pragma warning restore CS1998
            vectors[key] = vector;

#pragma warning disable CS1998
        public async ValueTask<float[]> GetVectorAsync(string key) =>
#pragma warning restore CS1998
            vectors.TryGetValue(key, out var vector) ? vector : null;

#pragma warning disable CS1998
        public async ValueTask<IEnumerable<string>> SearchAsync(float[] queryVector, int topK)
#pragma warning restore CS1998
        {
            var similarities = vectors
                .Select(kvp => new
                {
                    Key = kvp.Key,
                    Similarity = CosineSimilarity(queryVector, kvp.Value)
                })
                .OrderByDescending(x => x.Similarity)
                .Take(topK)
                .Select(x => x.Key);

            return similarities;
        }

        private static double CosineSimilarity(float[] a, float[] b)
        {
            double dotProduct = 0;
            double magnitudeA = 0;
            double magnitudeB = 0;

            for (int i = 0; i < a.Length; i++)
            {
                dotProduct += a[i] * b[i];
                magnitudeA += a[i] * a[i];
                magnitudeB += b[i] * b[i];
            }

            return dotProduct / (Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB));
        }
    }
}
