using MediatR;

namespace Template.Application.Command
{
    public class GetProductByIdQuery : IRequest<object>
    {
        public int Id { get; set; }
    }
}