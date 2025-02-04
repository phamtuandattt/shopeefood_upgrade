using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.SqlCommands
{
    public class StoreProcedure
    {
        public const string GET_BUSINESS_IN_THE_CITY = "EXEC GET_BUSINESS_IN_THE_CITY {0}";
        public const string GET_SHOP_BUSINESS_IN_THE_CITY = "EXEC GET_SHOP_BUSINESS_IN_THE_CITY {0}, {1}, {2}, {3}";
    }
}
