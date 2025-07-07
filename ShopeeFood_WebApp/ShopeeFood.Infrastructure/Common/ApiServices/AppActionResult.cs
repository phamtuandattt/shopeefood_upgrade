using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.Infrastructure.Common.ApiServices
{
    public class AppActionResult<TData, TError>
        where TData : class
        where TError : class
    {
        public bool IsSuccess { get; set; }

        public TData? Data { get; set; }

        public TError? Error { get; set; }

        public AppActionResult()
        {
            IsSuccess = false;
            Data = null;
            Error = null;
        }

        public void SetResult(TData data)
        {
            IsSuccess = true;
            Data = data;
        }

        public void SetError(TError error)
        {
            IsSuccess = false;
            Error = error;
        }
    }
}
