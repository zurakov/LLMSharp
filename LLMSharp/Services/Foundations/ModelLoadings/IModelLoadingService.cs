// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using LLMSharp.Models.ModelEngines;

namespace LLMSharp.Services.Foundations.ModelLoadings
{
    public interface IModelLoadingService
    {
        ValueTask<ModelInstance> LoadModelAsync(string modelPath);
        ValueTask UnloadModelAsync(ModelInstance model);
    }
}
