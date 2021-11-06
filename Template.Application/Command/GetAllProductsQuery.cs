using MediatR;

namespace Template.Application.Command
{
    public class GetAllProductsQuery : IRequest<object>
    {
        public object PageSize { get; set; }
        public object PageNumber { get; set; }
    }
}