using Configuration.Domain;
using Configuration.Repository;
using Configuration.Repository.Context;
using Core.Communication.Mediator;
using Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace Configuration.API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IWarehouseRepository, WarehouseRepository>();

            services.AddScoped<ConfigurationDataContext>();
        }
    }
}
