// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Exceptions
{
    public class TrainingException : LLMSharpException
    {
        public TrainingException() { }

        public TrainingException(string message)
            : base(message) { }

        public TrainingException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
