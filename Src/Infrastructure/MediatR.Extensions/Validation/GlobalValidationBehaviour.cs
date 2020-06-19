using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Validation.Interfaces;

namespace MediatR.Extensions.Validation
{
    public class GlobalValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IGlobalValidation> _globalValidations;

        public GlobalValidationBehaviour(IEnumerable<IGlobalValidation> globalValidations)
        {
            _globalValidations = globalValidations;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_globalValidations == null || !_globalValidations.Any())
                return await next();

            foreach (var globalValidation in _globalValidations)
            {
                if(globalValidation == null)
                    continue;

                await globalValidation.Validate(request);
            }

            return await next();
        }
    }
}
