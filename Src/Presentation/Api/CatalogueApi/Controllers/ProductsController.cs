using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Products.Queries.GetProduct;
using Domain.ApiModels;
using Domain.Authorization.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogueApi.Controllers
{
    [Authorize(Policy = AuthorizationPolicies.ReadPolicy)]
    public class ProductsController : BaseController
    {
        protected ProductsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> GetProduct([FromRoute] GetProductRequestFromRoute requestFromRoute, [FromQuery] GetProductRequestFromQuery requestFromQuery)
        {
            var apiResponse = new ApiResponse<ProductResponse>
            {
                Data = await Mediator.Send(new GetProductQuery
                {
                    Id = requestFromRoute.Id,
                    Date = requestFromQuery.Date
                })
            };

            return Ok(apiResponse);
        }
    }
}