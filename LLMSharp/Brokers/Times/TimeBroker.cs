// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Brokers.Times
{
    public class TimeBroker : ITimeBroker
    {
        public DateTimeOffset GetCurrentDateTimeOffset() => DateTimeOffset.UtcNow;

        public DateTime GetCurrentDateTime() => DateTime.UtcNow;
    }
}
