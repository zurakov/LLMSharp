// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LLMSharp.Models.Monitorings;

namespace LLMSharp.Brokers.Monitorings
{
    public class MonitoringBroker : IMonitoringBroker
    {
        private readonly List<InteractionEvent> interactionEvents = new();

#pragma warning disable CS1998
        public async ValueTask LogInteractionEventAsync(InteractionEvent interactionEvent) =>
#pragma warning restore CS1998
            interactionEvents.Add(interactionEvent);

#pragma warning disable CS1998
        public async ValueTask<IEnumerable<InteractionEvent>> GetAllInteractionEventsAsync() =>
#pragma warning restore CS1998
            interactionEvents.AsEnumerable();

#pragma warning disable CS1998
        public async ValueTask<IEnumerable<InteractionEvent>> GetInteractionEventsByTypeAsync(
#pragma warning restore CS1998
            InteractionType type) =>
            interactionEvents.Where(e => e.Type == type);
    }
}
