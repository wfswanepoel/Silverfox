using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Caching.Interfaces
{
    public interface ICacheInvalidator<in TRequest>
    {
        Task Invalidate(TRequest request);
    }
}
