// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Exceptions
{
    public class LLMSharpException : Exception
    {
        public LLMSharpException() { }

        public LLMSharpException(string message)
            : base(message) { }

        public LLMSharpException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
