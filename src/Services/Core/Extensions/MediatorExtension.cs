using Core.Communication.Mediator;
using Core.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace Core.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublishEvents(this IMediatorHandler mediator, DbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(entity => entity.Entity.Notifications != null && entity.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(entity => entity.Entity.Notifications)
                .ToList();

            domainEntities.ToList().ForEach(entity => entity.Entity.ClearEvent());

            var tasks = domainEvents
                .Select(async (domainEvents) =>
                {
                    await mediator.PublishEvent(domainEvents);
                });

            await Task.WhenAll(tasks);
        }
    }
}
