using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure.Validation.Interfaces;
using FluentValidation;

namespace Application.Infrastructure.Validation.RequestDate
{
    public class RequireDateValidation : IGlobalValidation
    {
        public async Task Validate<TRequest>(TRequest request)
        {
            if (!(request is IRequireDate requireDate))
                return;

            var validator = new RequireDateValidator();
            await validator.ValidateAndThrowAsync(requireDate);
        }
    }
}
