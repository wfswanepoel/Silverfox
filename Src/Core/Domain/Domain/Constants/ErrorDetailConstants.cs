using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Constants
{
    public class ErrorDetailConstants
    {
        public const string HeaderFieldRequired = "The Header Field Is Required";
        public const string HeaderFieldEmpty = "The Requested Header Field Cannot Be Null Or Empty";
        public const string HeaderFieldNotSupported = "The Requested Header Value Is Not Supported";
        public const string AuthorizationTokenExpired = "The Authorization Token Has Expired";
    }
}
