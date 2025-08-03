using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.Infrastructure.Common
{
    public class ApiResponse
    {
        public string? status { get; set; }
        public string? message { get; set; }
        public string? data { get; set; }
    }

    public class ApiModelResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } 

        public ApiModelResponse(bool IsSuccess, string message) 
        {
            this.IsSuccess = IsSuccess;
            this.Message = message;
        }
    }
}
