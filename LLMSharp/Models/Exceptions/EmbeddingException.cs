// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Exceptions
{
    public class EmbeddingException : LLMSharpException
    {
        public EmbeddingException() { }

        public EmbeddingException(string message)
            : base(message) { }

        public EmbeddingException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
