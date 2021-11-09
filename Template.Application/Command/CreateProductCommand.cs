using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Template.Application.DTOs;

namespace Application.Features.Products.Commands.CreateProduct
{
    public partial class CreateProductCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        public CreateProductCommandHandler( IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<Response<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult( new Response<int>(1));
        }
    }
}
