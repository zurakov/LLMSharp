// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

namespace LLMSharp.Models.Streamings
{
    public enum StreamingEventType
    {
        Started,
        TokenReceived,
        Completed,
        Error
    }
}
