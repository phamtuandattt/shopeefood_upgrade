using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.Infrastructure.Common
{
    public static class ApiResponseMessage
    {
        public const string NO_AVAILABLE = "No available !";
        public const string INVALID_OBJECT = "Invalid object data !";
        public const string NOT_FOUND = "Not found !";
        public const string SUCCESS = "Success !";
        public const string BAD_REQUEST = "The list of objects cannot be null or empty !";
        public const string INVALID_CREDENTAILS = "Invalid credentials";
        public const string IS_EXISTED = "Object already exists";
        public const string REGISTERED_SUCCESS = "User registered successfully";
        public const string INVALID_REFRESH = "Invalid refresh token";
        public const string INVALID_TOKEN = "Invalid token";
    }
}
