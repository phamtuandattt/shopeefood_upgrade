﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopeeFood.Infrastructure.Common.Validate;
using ShopeeFood.Infrastructure.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace ShopeeFood.Infrastructure.Common.ApiServices
{
    public class RestServices
    {
        public const string PROPERTY_AUTHENTICATION_TYPE = "AuthenticationType";
        public const string PROPERTY_USERNAME = "Username";
        public const string PROPERTY_PASSWORD = "Password";
        public const string PROPERTY_TOKEN = "Token";

        private const string AUTH_TYPE_BASIC = "Basic";
        private const string AUTH_TYPE_BEARER = "Bearer";
        private const string AUTH_TYPE_SSWS = "SSWS";

        private IDictionary<string, string> _contentHeaders;
        private IDictionary<string, string> _authorizationProperties;
        private SchemeAuthorizationType _schemeAuthorizationType;

        private IHttpContextAccessor _HttpContextAccessor { get; } = null!;

        public RestServices(IHttpContextAccessor HttpContextAccessor)
        {
            _contentHeaders = new Dictionary<string, string>();
            _authorizationProperties = new Dictionary<string, string>();

            _HttpContextAccessor = HttpContextAccessor;
        }

        public async Task<AppActionResult<string, string>> GetAsync(IDictionary<string, object> data, string apiUrl)
        {
            var result = new AppActionResult<string, string>();
            try
            {
                var aHandler = new HttpClientHandler(); // { ClientCertificateOptions = ClientCertificateOption.Automatic };
                aHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                var httpClient = new HttpClient(aHandler);
                InitHttpClient(httpClient);

                string uri = data == null ? apiUrl : GetUriWithparameters(apiUrl, data);
                //var uriLogging = SanitizeSensitiveUriValue(uri);

                Logger.Debug($"Request API Get: {uri}");
                var response = await httpClient.GetAsync(uri);
                await BuildHttpResult(response, result);

            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
                Logger.Error("ERROR Get API: ", ex);
            }
            return result;
        }

        public async Task<AppActionResult<string, string>> PostAsync(JObject json, string apiUrl)
        {
            var result = new AppActionResult<string, string>();
            try
            {
                var data = json != null ? json.ToString(Formatting.None, null) : string.Empty;

                var urlLogging = SanitizeSensitiveUriValue(apiUrl);
                Logger.Debug($"Request API Post: {urlLogging}");

                var content = new StringContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue(("application/json"));

                //var aHandler = new HttpClientHandler { ClientCertificateOptions = ClientCertificateOption.Automatic };
                var aHandler = new HttpClientHandler(); // { ClientCertificateOptions = ClientCertificateOption.Automatic };
                aHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                var httpClient = new HttpClient(aHandler);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                InitHttpClient(httpClient);

                await ExecuteHttpAction<StringContent>(apiUrl, content, httpClient.PostAsync, result);
            }
            catch (Exception ex)
            {
                result.SetError(ex.Message);
                Logger.Error("ERROR Post API: ", ex);
            }
            return result;
        }


        private void InitHttpClient(HttpClient httpClient)
        {
            AddContentHeaders(httpClient);
            SetAuthorization(httpClient);
        }


        private async Task ExecuteHttpAction<TContent>(string uri, TContent content,
        Func<string, TContent, Task<HttpResponseMessage>> httpAction,
        AppActionResult<string, string> result)
        {
            HttpResponseMessage response = await httpAction(uri, content);
            await BuildHttpResult(response, result);
        }

        private async Task BuildHttpResult(HttpResponseMessage response, AppActionResult<string, string> result)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                result.SetResult(responseString);
            }
            else
            {
                result.SetError(responseString);
            }
        }

        private void AddContentHeaders(HttpClient httpClient)
        {
            foreach (var headerItem in _contentHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
            }
        }

        private void SetAuthorization(HttpClient httpClient)
        {
            try
            {
                if (_authorizationProperties == null || !_authorizationProperties.Any())
                {
                    return;
                }
                switch (_schemeAuthorizationType)
                {
                    case SchemeAuthorizationType.Basic:
                        string username = _authorizationProperties[PROPERTY_USERNAME];
                        string password = _authorizationProperties[PROPERTY_PASSWORD];

                        httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue(AUTH_TYPE_BASIC, Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
                        break;
                    case SchemeAuthorizationType.Bearer:
                        string bearerToken = _authorizationProperties[PROPERTY_TOKEN];
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AUTH_TYPE_BEARER, bearerToken);
                        break;
                    case SchemeAuthorizationType.SSWS:
                        string sswsToken = _authorizationProperties[PROPERTY_TOKEN];
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AUTH_TYPE_SSWS, sswsToken);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"ERROR: SetAuthorization: ", ex);
            }
        }

        /// <summary>
        /// Return the full URL string with query parameters and port number applied
        /// </summary>
        /// <param name="uri">The base URI</param>
        /// <param name="queryParams">A Dictionary of query parameters</param>
        /// <param name="port"></param>
        /// <returns></returns>
        private string GetUriWithparameters(string uri, IDictionary<string, object> queryParams = null, int port = -1)
        {
            var builder = new UriBuilder(uri) { Port = port }; // Allows construct or modify URIs
            if (null != queryParams && 0 < queryParams.Count)
            {
                var query = HttpUtility.ParseQueryString(builder.Query); // Converts it into a NameValueCollection you can manipulate like a dictionary.
                foreach (var item in queryParams)
                {
                    if (!string.IsNullOrEmpty(item.Value?.ToString()))
                    {
                        query[item.Key] = item.Value.ToString();
                    }
                }
                builder.Query = query.ToString();
            }
            return builder.Uri.ToString();
        }

        /// <summary>
        /// Protect sensitive information 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string SanitizeSensitiveUriValue(string url)
        {
            if (url.Contains(System.Net.WebUtility.UrlEncode("@")))
            {
                url = System.Net.WebUtility.UrlDecode(url);
            }
            var resultUrl = url;
            try
            {
                var isUrlWell = Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
                if (isUrlWell)
                {
                    var myUri = new Uri(url);
                    var dicParam = HttpUtility.ParseQueryString(myUri.Query);
                    if (dicParam.Count == 0)
                    {
                        if (myUri.Segments.Any(x => x.Contains("@")))
                        {
                            var replaceValue = myUri.Segments.Where(x => x.Contains("@"));
                            foreach (var item in replaceValue)
                            {
                                //var decodedValue = HttpUtility.UrlEncode(item);
                                var sanitizeValue = SanitizeEmailValue(item);
                                resultUrl = resultUrl.Replace(item, sanitizeValue);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dicParam.Count; i++)
                        {
                            if (dicParam.GetKey(i) == "username" || dicParam.GetKey(i) == "email" || dicParam.GetKey(i) == "emailAddress")
                            {
                                var itemValue = dicParam.Get(i);
                                var decodedValue = HttpUtility.UrlEncode(itemValue);
                                var sanitizeValue = SanitizeEmailValue(itemValue);
                                resultUrl = resultUrl.Replace(decodedValue, sanitizeValue);
                            }
                            if (dicParam.GetKey(i) == "password")
                            {
                                resultUrl = resultUrl.Replace(dicParam.Get(i), "*********");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"SanitizeSensitiveUriValue Error: {ex.ToString()}");
            }

            return resultUrl;
        }

        private static string SanitizeValue(string value)
        {
            var sanitizeValue = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                sanitizeValue.Append("x");
            }
            return sanitizeValue.ToString();
        }

        private static string SanitizeEmailValue(string value)
        {
            var result = "xxxx";
            try
            {
                if (value.Contains(WebUtility.UrlEncode("@")))
                {
                    value = WebUtility.UrlDecode(value);
                }
                var emailAddress = new System.Net.Mail.MailAddress(value);
                var domainEmail = emailAddress.Host;
                var sanitizeValue = SanitizeValue(emailAddress.Host);
                result = value.Replace(domainEmail, sanitizeValue.ToString());
                //hide some text at first
                result = result.Replace(value.Substring(0, 3), "xxx");
            }
            catch (FormatException ex)
            {
                Logger.Error($"SanitizeEmailValue Error: {ex.Message}");
                return Constants.Constants.ErrorInvalidEmail;
            }
            catch (Exception ex)
            {
                Logger.Error($"SanitizeEmailValue Error: {ex.ToString()}");
            }
            return result;
        }



        #region Set Header and Authourization type

        public void SetHeaders(IDictionary<string, string> contentHeaders)
        {
            Check.NotNull(contentHeaders, nameof(contentHeaders));
            _contentHeaders = contentHeaders;
        }

        public void SetBasicAuthorization(string userName, string password)
        {
            Check.NotNull(userName, nameof(userName));
            Check.NotNull(password, nameof(password));

            _schemeAuthorizationType = SchemeAuthorizationType.Basic;
            _authorizationProperties = new Dictionary<string, string>
            {
                { PROPERTY_USERNAME, userName },
                { PROPERTY_PASSWORD, password }
            };
        }

        public void SetBearerAuthorization(string token)
        {
            Check.NotNull(token, nameof(token));
            _schemeAuthorizationType = SchemeAuthorizationType.Bearer;
            _authorizationProperties = new Dictionary<string, string>
            {
                { PROPERTY_TOKEN, token }
            };
        }

        public void SetAuthorization2_0(string token)
        {
            Check.NotNull(token, nameof(token));
            _authorizationProperties = new Dictionary<string, string>
            {
                { PROPERTY_TOKEN, token }
            };
        }


        public void SetSSWSAuthorization(string token)
        {
            Check.NotNull(token, nameof(token));

            _schemeAuthorizationType = SchemeAuthorizationType.SSWS;
            _authorizationProperties = new Dictionary<string, string>
            {
                { PROPERTY_TOKEN, token }
            };
        }

        #endregion

        public async Task<AppActionResult<TResult, TError>> GetAsync<TResult, TError>(
            IDictionary<string, object> data, string apiUrl)
            where TResult : class
            where TError : class
        {
            return await DoAction<IDictionary<string, object>, TResult, TError>(GetAsync, data, apiUrl, "[GET]");
        }

        public async Task<AppActionResult<TResult, TError>> PostAsync<TResult, TError>(JObject json, string apiUrl)
           where TResult : class
           where TError : class
        {
            return await DoAction<JObject, TResult, TError>(PostAsync, json, apiUrl, "[POST]");
        }

        private async Task<AppActionResult<TResultData, TError>> DoAction<TInput, TResultData, TError>(
                 Func<TInput, string, Task<AppActionResult<string, string>>> doActionFunc, TInput data, string apiUrl, string httpMethod)
                 where TResultData : class
                 where TError : class
        {
            var finalResult = new AppActionResult<TResultData, TError>();
            var response = await doActionFunc(data, apiUrl);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Data);

            if (response.IsSuccess)
            {
                try
                {
                    var dataResult = JsonConvert.DeserializeObject<TResultData>(apiResponse.Data);
                    if (dataResult != null)
                    {
                        finalResult.SetResult(dataResult);
                    }
                }
                catch (Exception ex)
                {

                    string errorMsg = $"Exception during deserialization to {typeof(TResultData)}: {ex.Message}";
                    Logger.Error(errorMsg, ex);

                    if (typeof(TError).Name == typeof(string).Name)
                    {
                        finalResult.SetError(errorMsg as TError);
                    }
                }

            }
            else
            {
                if (typeof(TError).Name == typeof(String).Name)
                {
                    finalResult.SetError(response.Error as TError);
                }
                else
                {
                    var errorResult = JsonConvert.DeserializeObject<TError>(response.Error);
                    if (errorResult != null)
                    {
                        finalResult.SetError(errorResult);
                    }
                    else
                    {
                        Logger.Error($"ERROR while deserializing object to type: {typeof(TError)}");
                    }
                }
            }
            return finalResult;
        }

    }

    public enum SchemeAuthorizationType
    {
        Basic = 1,
        Bearer = 2,
        SSWS = 3
    }
}
