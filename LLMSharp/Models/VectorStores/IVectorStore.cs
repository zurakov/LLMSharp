// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace LLMSharp.Models.VectorStores
{
    public interface IVectorStore
    {
        ValueTask SaveVectorAsync(string key, float[] vector);
        ValueTask<float[]> GetVectorAsync(string key);
        ValueTask<IEnumerable<string>> SearchAsync(float[] vector, int topK);
    }
}
