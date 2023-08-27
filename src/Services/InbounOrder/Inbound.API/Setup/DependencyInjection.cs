using Core.Communication.Mediator;
using Core.Messages.CommonMessages.Notifications;
using Inbound.Application.Commands;
using Inbound.Application.Queries;
using Inbound.Domain;
using Inbound.Infrastructure.Repository;
using MediatR;

namespace Inbound.API.Setup
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
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderQueries, OrderQueries>();
            services.AddTransient<IRequestHandler<UpdateOrderCommand, bool>, OrderCommandHandler>();
            services.AddTransient<IRequestHandler<CreateOrderCommand, bool>, OrderCommandHandler>();
            //services.AddTransient<IRequestHandler<DeleteOrderCommand, bool>, OrderCommandHandler>();
        }
    }
}
