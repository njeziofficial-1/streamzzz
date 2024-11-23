using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Infrastructure.Constant
{
    public class Constants
    {
        public static class Collections
        {
            public const string OtpVerification = "OtpVerification";
            public const string Login = "Login";
            public const string SignUp = "SignUp";
            public const string Dashboard = "Dashboard";
            public const string ResetPassword = "ResetPassword";
            public const string ProfileSetup = "ProfileSetup";
            public const string OrderDetail = "OrderDetail";
        }

        public static class QueryParams
        {
            public const string CreatedOn = "createdOn";
            public const string GreaterThan = "$gt";
            public const string LessThan = "$lt";
            public const string IsDeleted = "isDeleted";
            public const string Matches = "$eq";
        }

    }
}
