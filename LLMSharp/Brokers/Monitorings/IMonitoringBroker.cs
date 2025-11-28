// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using LLMSharp.Models.Monitorings;

namespace LLMSharp.Brokers.Monitorings
{
    public interface IMonitoringBroker
    {
        Task LogInteractionEventAsync(InteractionEvent interactionEvent);
        Task<IEnumerable<InteractionEvent>> GetAllInteractionEventsAsync();
        Task<IEnumerable<InteractionEvent>> GetInteractionEventsByTypeAsync(InteractionType type);
    }
}
