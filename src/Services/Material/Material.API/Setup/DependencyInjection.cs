using Core.Communication.Mediator;
using Core.Messages.CommonMessages.Notifications;
using Material.Application.Commands;
using Material.Application.Events;
using Material.Application.Queries;
using Material.Domain;
using Material.Infrastructure.Repository;
using MediatR;

namespace Material.API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Order
            services.AddTransient<IMaterialRepository, MaterialRespository>();
            services.AddTransient<IMaterialQueries, MaterialQueries>();
            services.AddTransient<IRequestHandler<UpdateProductCommand, bool>, MaterialCommandHandler>();
            services.AddTransient<INotificationHandler<UpdatedProductEvent>, MaterialEventHandler>();
        }
    }
}
