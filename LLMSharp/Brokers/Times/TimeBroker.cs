// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

namespace LLMSharp.Brokers.Times
{
    public class TimeBroker : ITimeBroker
    {
        public DateTimeOffset GetCurrentDateTimeOffset()
        {
            return DateTimeOffset.UtcNow;
        }

        public DateTime GetCurrentDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
