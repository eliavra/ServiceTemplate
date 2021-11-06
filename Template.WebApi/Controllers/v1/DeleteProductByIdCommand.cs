using MediatR;

namespace Template.WebApi.Controllers.v1
{
    internal class DeleteProductByIdCommand : IRequest<object>
    {
        public int Id { get; set; }
    }
}