using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Authorization.Models
{
    public class AuthorizationPolicies
    {
        public const string AdministratorPolicy = "AdministratorPolicy";
        public const string ReadPolicy = "ReadPolicy";
        public const string WriteProductPolicy = "WriteProductPolicy";
        public const string WriteCategoriesPolicy = "WriteCategoriesPolicy";
        public const string WriteStoresPolicy = "WriteStoresPolicy";
        public const string WriteBrandsPolicy = "WriteBrandsPolicy";
        public const string WriteGiftcardsPolicy = "WriteGiftcardsPolicy";
    }
}
