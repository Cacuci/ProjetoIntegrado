using Configuration.Domain;
using Core.Communication.Mediator;
using Core.Messages;
using Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace Configuration.Application.Commands
{
    public class UserCommandHandler : IRequestHandler<CreateUserCommand, bool>,
                                      IRequestHandler<UpdateUserCommand, bool>,
                                      IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public UserCommandHandler(IUserRepository userRepository, IMediatorHandler mediatorHandler)
        {
            _userRepository = userRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
            {
                return false;
            }

            var user = await _userRepository.GetUserByEmail(request.Email);

            if (user is not null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Email", "Email já cadastrado"));

                return false;
            }

            var userModel = new User(request.Name, request.Email);

            await _userRepository.CreateUserAsync(userModel, request.Password);

            return true;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
            {
                return false;
            }

            var user = await _userRepository.GetUserById(request.ID);

            if (user is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("ID", "Usuário não encontrado"));

                return false;
            }

            user.UserName = request.Name;

            await _userRepository.UpdateUserAsync(user);

            return true;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
            {
                return false;
            }

            var user = await _userRepository.GetUserById(request.ID);

            if (user is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("ID", "Usuário não encontrado"));

                return false;
            }

            await _userRepository.DeleteUserAsync(user);

            return true;
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid())
            {
                return true;
            }
            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
