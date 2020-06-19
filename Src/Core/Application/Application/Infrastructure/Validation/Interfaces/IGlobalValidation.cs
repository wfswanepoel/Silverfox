using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Validation.Interfaces
{
    public interface IGlobalValidation
    {
        Task Validate<TRequest>(TRequest request);
    }
}
