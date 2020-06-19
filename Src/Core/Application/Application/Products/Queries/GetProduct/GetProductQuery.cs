using System;
using System.Collections.Generic;
using System.Text;
using Application.Infrastructure.Validation;
using Application.Infrastructure.Validation.Interfaces;
using MediatR;

namespace Application.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<ProductResponse>, IRequireCulture, IRequireDate
    {
        public string Id { get; set; }
        public string CultureName { get; set; }
        public DateTime Date { get; set; }
    }
}
