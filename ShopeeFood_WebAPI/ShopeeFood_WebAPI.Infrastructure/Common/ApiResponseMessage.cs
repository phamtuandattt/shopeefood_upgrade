using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.Infrastructure.Common
{
    public static class ApiResponseMessage
    {
        public static IDictionary<ErrorCode, string> Messages;

        static ApiResponseMessage()
        {
            Messages = new Dictionary<ErrorCode, string>
            {
                {ErrorCode.Continue, "Continuew"},
                {ErrorCode.SwitchingProtocols, "Switching Protocols"},
                {ErrorCode.Processing, "Processing"},
                {ErrorCode.OKSuccess, "OK/Success"},
                {ErrorCode.Created, "Created"},
                {ErrorCode.Accepted,  "Accepted"},
                {ErrorCode.NonAuthoritativeInfo,  "Non-Authoritative Info"},
                {ErrorCode.NoContent,  "No Content"},
                {ErrorCode.ResetContent,  "Reset Content"},
                {ErrorCode.PartialContent,  "Partial Content"},
                {ErrorCode.MultipleChoices,  "Multiple Choices"},
                {ErrorCode.MovedPermanently,  "Moved Permanently"},
                {ErrorCode.Found,  "Found"},
                {ErrorCode.SeeOther,  "See Other"},
                {ErrorCode.NotModified,  "Not Modified"},
                {ErrorCode.TemporaryRedirect,  "Temporary Redirect"},
                {ErrorCode.PermanentRedirect,  "Permanent Redirect"},
                {ErrorCode.BadRequest,  "Bad Request"},
                {ErrorCode.Unauthorized,  "Unauthorized"},
                {ErrorCode.PaymentRequired,  "Payment Required"},
                {ErrorCode.Forbidden,  "Forbidden"},
                {ErrorCode.NotFound,  "Not Found"},
                {ErrorCode.MethodNotAllowed,  "Method Not Allowed"},
                {ErrorCode.InternalServerError,  "InternalServerError"},
                {ErrorCode.NotImplemented,  "Not Implemented"},
                {ErrorCode.BadGateway,  "Bad Gateway"}
            };
        }

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

    public enum ErrorCode
    {
        Continue = 101,
        SwitchingProtocols = 102,
        Processing = 103,

        OKSuccess = 200,
        Created = 201,
        Accepted = 202,
        NonAuthoritativeInfo = 203,
        NoContent = 204,
        ResetContent = 205,
        PartialContent = 206,

        MultipleChoices = 300,
        MovedPermanently = 301,
        Found = 302,
        SeeOther = 303,
        NotModified = 304,
        TemporaryRedirect = 307,
        PermanentRedirect = 308,

        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        NotAcceptable = 406,
        RequestTimeout = 408,
        Conflict = 409,
        Gone = 410,
        LengthRequired = 411,
        PreconditionFailed = 412,
        PayloadTooLarge = 413,
        URITooLong = 414,
        UnsupportedMediaType = 415,
        UnprocessableEntity = 422,
        TooManyRequests	=	429,

        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
        HTTPVersionNotSupported = 505,
    }
}
