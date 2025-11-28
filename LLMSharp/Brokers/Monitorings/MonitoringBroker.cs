// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using LLMSharp.Models.Monitorings;

namespace LLMSharp.Brokers.Monitorings
{
    public class MonitoringBroker : IMonitoringBroker
    {
        private readonly List<InteractionEvent> interactionEvents = new();

        public async Task LogInteractionEventAsync(InteractionEvent interactionEvent)
        {
            await Task.Run(() => interactionEvents.Add(interactionEvent));
        }

        public async Task<IEnumerable<InteractionEvent>> GetAllInteractionEventsAsync()
        {
            return await Task.FromResult(interactionEvents.AsEnumerable());
        }

        public async Task<IEnumerable<InteractionEvent>> GetInteractionEventsByTypeAsync(
            InteractionType type)
        {
            var filteredEvents = interactionEvents.Where(e => e.Type == type);
            return await Task.FromResult(filteredEvents);
        }
    }
}
