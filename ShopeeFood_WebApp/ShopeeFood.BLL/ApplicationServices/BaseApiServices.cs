using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ShopeeFood.Infrastructure.Common.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ApplicationServices
{
    public class BaseApiServices
    {
        protected RestServices RestServices { get; set; }
        protected IHttpContextAccessor _httpContextAccessor { get; set; }
        protected readonly IConfiguration _configuration;


        protected string ApiDomain
        {
            get => _configuration["ApiDomain"]; 
        }


        public BaseApiServices(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            RestServices = _httpContextAccessor.HttpContext.RequestServices.GetService<RestServices>();
            //ApiDomain = _configuration["ApiDomain"];
        }

        protected virtual JObject SerializeParams<TInput>(TInput reqParams)
        {
            if (reqParams == null)
            {
                return null;
            }

            var stringParams = JsonConvert.SerializeObject(reqParams,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return JObject.Parse(stringParams);
        }
    }
}
