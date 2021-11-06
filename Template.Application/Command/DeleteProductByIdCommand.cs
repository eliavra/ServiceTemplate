using MediatR;

namespace Template.Application.Command
{
    public class DeleteProductByIdCommand : IRequest<object>
    {
        public int Id { get; set; }
    }
}