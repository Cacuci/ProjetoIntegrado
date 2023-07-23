using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Core.Messages
{
    public class Command : Message, IRequest<bool>
    {
        public DateTime TimeStamp { get; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
