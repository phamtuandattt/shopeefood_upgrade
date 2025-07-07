using ShopeeFood.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.Infrastructure.Common.Validate
{
    public static class Check
    {
        public static void NotNull<T>(T argument, string paramName) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void NotEmpty(string argument, string paramName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                var message = $"{paramName} cannot be null or empty.";
                Logger.Error(message);
                throw new ArgumentException(message, paramName);
            }
        }

        public static void NotEmpty(Guid argument, string paramName)
        {
            if (argument == Guid.Empty)
            {
                var message = $"{paramName} cannot be empty.";
                Logger.Error(message);
                throw new ArgumentException(message, paramName);
            }
        }
    }
}
