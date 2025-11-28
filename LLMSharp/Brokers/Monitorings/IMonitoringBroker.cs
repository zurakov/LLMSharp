// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using LLMSharp.Models.Monitorings;

namespace LLMSharp.Brokers.Monitorings
{
    public interface IMonitoringBroker
    {
        ValueTask LogInteractionEventAsync(InteractionEvent interactionEvent);
        ValueTask<IEnumerable<InteractionEvent>> GetAllInteractionEventsAsync();
        ValueTask<IEnumerable<InteractionEvent>> GetInteractionEventsByTypeAsync(InteractionType type);
    }
}
