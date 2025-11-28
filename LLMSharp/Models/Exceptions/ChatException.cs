// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Models.Exceptions
{
    public class ChatException : LLMSharpException
    {
        public ChatException() { }

        public ChatException(string message)
            : base(message) { }

        public ChatException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
