using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Products.CreateProduct
{
    public record CreateProductCommand(string Name, decimal Price) : IRequest<Result<Guid>>;

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The name is mandatory.")
                .MaximumLength(100).WithMessage("The name cannot exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("The price must be greater than zero.");
        }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        /*private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }*/

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productResult = ProductEntity.Create(request.Name, request.Price);

            if (productResult.IsFailure)
            {
                return Result.Failure<Guid>(productResult.Error);
            }

            var product = productResult.Value;

            // TODO: Guardar en repositorio (se conectará en la capa de Infrastructure)
            //await _productRepository.AddAsync(product, cancellationToken);

            return Result.Success(product.Id);
        }
    }
}
