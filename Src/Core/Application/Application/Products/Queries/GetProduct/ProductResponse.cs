using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.Images;
using Domain.Entities.Prices;

namespace Application.Products.Queries.GetProduct
{
    [Serializable]
    public class ProductResponse
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ImageResponse> Images { get; set; } = new List<ImageResponse>();
        public List<PriceResponse> Prices { get; set; } = new List<PriceResponse>();
    }
}
