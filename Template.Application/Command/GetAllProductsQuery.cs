using MediatR;

namespace Template.Application.Command
{
    public class GetAllProductsQuery : IRequest<object>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}