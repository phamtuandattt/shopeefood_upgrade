using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.Infrastructure.Common.SessionManagement
{
    public class ClientSession
    {
        #region refer: https://stackoverflow.com/questions/64799591/is-there-a-high-performance-way-to-replace-the-binaryformatter-in-net5

        #endregion
        private HttpContext _current;

        IHttpContextAccessor HttpContextAccessor;


        //public ClientSession(HttpContext context)
        public ClientSession(IHttpContextAccessor _HttpContextAccessor)
        {
            HttpContextAccessor = _HttpContextAccessor;

            _current = HttpContextAccessor.HttpContext;

        }

        public ClientSession(HttpContext context)
        {
            _current = context;

        }


        #region Session is string
        public string AccessToken
        {
            get
            {
                return _current.Session.GetString("AccessToken") as string;
            }
            set
            {
                _current.Session.SetString("AccessToken", value);
            }
        }

        public string TokenResetpassword
        {
            get
            {
                return _current.Session.GetString("TokenResetpassword") as string;
            }
            set
            {
                _current.Session.SetString("TokenResetpassword", value);
            }
        }


        #endregion


        #region Session is int
        public int ExampleSessionInt
        {
            get
            {
                return _current.Session.GetInt32("ExampleSessionInt") ?? default;
            }
            set
            {
                _current.Session.SetInt32("ExampleSessionInt", value);
            }
        }


        #endregion


        #region Session is bool
        public bool IsLogin
        {
            get
            {
                var byteArray = _current.Session.Get("IsLogin");
                if (byteArray is null || byteArray.Any() == false)
                {
                    return false;
                }

                return SessionExtensions.GetObjectFromByte<bool>(byteArray);
            }
            set
            {
                var obj = SessionExtensions.GetByteFromObject<bool>(value);

                _current.Session.Set("IsLogin", obj);
            }
        }

        #endregion


        #region Session is object
        public UserProfileModel? CurrentUser
        {
            get
            {
                var byteArray = _current.Session.Get("CurrentUser");
                if (byteArray is null || byteArray.Any() == false)
                {
                    return new UserProfileModel();
                }

                var obj = SessionExtensions.GetObjectFromByte<UserProfileModel>(byteArray);

                return obj;
            }
            set
            {
                var byteArray = SessionExtensions.GetByteFromObject<UserProfileModel>(value);

                _current.Session.Set("CurrentUser", byteArray);
            }
        }

        #endregion

    }
}
