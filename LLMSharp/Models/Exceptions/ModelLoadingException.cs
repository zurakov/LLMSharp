// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Exceptions
{
    public class ModelLoadingException : LLMSharpException
    {
        public ModelLoadingException() { }

        public ModelLoadingException(string message)
            : base(message) { }

        public ModelLoadingException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
