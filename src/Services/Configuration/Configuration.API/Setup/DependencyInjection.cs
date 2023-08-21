using Configuration.Application.Commands;
using Configuration.Application.Queries;
using Configuration.Domain;
using Configuration.Infrastructure.Context;
using Configuration.Infrastructure.Repository;
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

            // User
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserQueries, UserQueries>();
            services.AddTransient<IRequestHandler<UpdateUserCommand, bool>, UserCommandHandler>();
            services.AddTransient<IRequestHandler<CreateUserCommand, bool>, UserCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, UserCommandHandler>();

            // Warehouse
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            services.AddTransient<IWarehouseQueries, WarehouseQueries>();
            services.AddTransient<IRequestHandler<UpdateWarehouseCommand, bool>, WarehouseCommandHandler>();
            services.AddTransient<IRequestHandler<CreateWarehouseCommand, bool>, WarehouseCommandHandler>();

            services.AddTransient<ConfigurationDataContext>();
        }
    }
}
