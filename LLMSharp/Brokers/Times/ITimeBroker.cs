// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Brokers.Times
{
    public interface ITimeBroker
    {
        DateTimeOffset GetCurrentDateTimeOffset();
        DateTime GetCurrentDateTime();
    }
}
