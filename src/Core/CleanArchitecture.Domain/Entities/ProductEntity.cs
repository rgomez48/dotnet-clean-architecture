using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Entities
{
    public class ProductEntity : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }

        private ProductEntity() { }

        private ProductEntity(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public static Result<ProductEntity> Create(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<ProductEntity>(new Error(
                    "Product.EmptyName",
                    "The product name cannot be empty.",
                    ErrorType.Validation));
            }

            if (price <= 0)
            {
                return Result.Failure<ProductEntity>(new Error(
                    "Product.InvalidPrice",
                    "The price must be greater than zero.",
                    ErrorType.Validation));
            }

            return Result.Success(new ProductEntity(name, price));
        }
    }
}
