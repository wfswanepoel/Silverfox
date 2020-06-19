using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Errors
{
    public enum ErrorTypes
    {
        UnknownError = 0,
        ValidationError = 1,
        WebRequestError = 2,
        AuthorizationError = 3,
        ReferenceError = 4,
        ClientManagementError = 5,
        AuthenticationError = 6,
        NoResultError = 7,
        InternalServerError = 8
    }
}
