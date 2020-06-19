using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.HttpClients.Interfaces;
using Application.Infrastructure.Persistence.Interfaces;
using AutoMapper;
using Domain.Entities.Images;
using Domain.Http.Clients.ContentDeliveryApi;
using Domain.Http.Enums;
using Domain.Http.Models;
using Domain.Persistence.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Products.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponse>
    {
        private readonly IDataExecutorFactory _dataExecutorFactory;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IContentDeliveryApiService _contentDeliveryApiService;

        public GetProductQueryHandler(IDataExecutorFactory dataExecutorFactory, IMapper mapper, IConfiguration configuration, IContentDeliveryApiService contentDeliveryApiService)
        {
            _dataExecutorFactory = dataExecutorFactory;
            _mapper = mapper;
            _configuration = configuration;
            _contentDeliveryApiService = contentDeliveryApiService;
        }

        public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            ContentDeliveryApiSingleResponse contentDeliveryApiSingleResponse;
            using (_contentDeliveryApiService)
            {
                contentDeliveryApiSingleResponse =
                    await _contentDeliveryApiService.GetSingleContent<ContentDeliveryApiSingleResponse>(
                        new ContentDeliveryApiSingleRequest
                        {
                            CancellationToken = cancellationToken,
                            ContentId = request.Id,
                            CultureName = request.CultureName,
                        });
            }

            if (contentDeliveryApiSingleResponse == null)
                return null; //Should throw exception for middle layer to handle for a better response message

            var productResponse = _mapper.Map<ProductResponse>(contentDeliveryApiSingleResponse);

            using (_dataExecutorFactory)
            {
                var productImages = await _dataExecutorFactory.ExecuteMany<Image>(PersistenceDatabases.Commerce,
                    "sp_GetProductImages", command =>
                    {
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = request.Id;
                    });

                if (productImages != null)
                    productResponse.Images = _mapper.Map<List<ImageResponse>>(productImages);
            }
            
            return productResponse;
        }
    }
}
