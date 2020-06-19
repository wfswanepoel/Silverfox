using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure.Validation.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Infrastructure.Validation.CultureName
{
    public class RequireCultureValidation : IGlobalValidation
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequireCultureValidation(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Validate<TRequest>(TRequest request)
        {
            if (!(request is IRequireCulture requireCulture))
                return;

            requireCulture.CultureName = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"];

            var validator = new RequireCultureValidator();
            await validator.ValidateAndThrowAsync(requireCulture);
        }
    }
}
