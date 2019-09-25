using David.Products.Common.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Common.Helpers
{
    public class ApiService
    {
        public static Response<object> Get<T>(string SiteBase, string baseAPI, string action, string token)
        {
            Response<object> dataResponse = new Response<object>();
            dataResponse.IsSuccess = false;
            try
            {
                var client = new RestClient(SiteBase);
                string uri = string.Format("{0}/{1}", baseAPI, action);
                var request = new RestRequest(uri, Method.GET);
                if (!string.IsNullOrEmpty(token))
                {
                    request.AddHeader("Token", token);
                }

                IRestResponse response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    dataResponse.Message = new System.Collections.Generic.List<MessageResult> { new MessageResult{
                        Message=response.StatusCode.ToString()} };
                }
                if (response.Content != null)
                {
                    var result = response.Content.ToString();
                    var list = JsonConvert.DeserializeObject<T>(result);
                    dataResponse.IsSuccess = true;
                    dataResponse.Result = list;
                }
            }
            catch (System.Exception ex)
            {
                dataResponse.Message.Add(new MessageResult { Message = "No se pudo realizar la petición" + ex });
            }
            return dataResponse;
        }

        public static Response<object> Post<T>(string SiteBase, string baseAPI, string action, string token, dynamic model)
        {
            Response<object> data = new Response<object>
            {
                IsSuccess = false,
            };
            try
            {
                var client = new RestClient(SiteBase);
                string uri = string.Format("{0}/{1}", baseAPI, action);
                var request = new RestRequest(uri, Method.POST);
                if (token != null)
                {
                    request.AddHeader("Token", token);
                }
                if (model != null)
                {
                    request.AddJsonBody(model);
                }
                IRestResponse response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    data.Message.Add(new MessageResult { Message = response.StatusCode.ToString() });
                }

                if (response.Content != null)
                {
                    var result = response.Content.ToString();
                    var list = JsonConvert.DeserializeObject<T>(result);
                    data.IsSuccess = true;
                    data.Result = list;
                }

                return data;
            }
            catch (System.Exception ex)
            {
                data.Message.Add(new MessageResult { Message = "No se pudo realizar la petición" + ex });
            }
            return data;
        }

        public static Response<object> Put<T>(string SiteBase, string baseAPI, string action, string token, dynamic model)
        {
            Response<object> data = new Response<object>
            {
                IsSuccess = false,
            };
            try
            {
                var client = new RestClient(SiteBase);
                string uri = string.Format("{0}/{1}", baseAPI, action);
                var request = new RestRequest(uri, Method.PUT);
                if (token != null)
                {
                    request.AddHeader("Token", token);
                }
                if (model != null)
                {
                    request.AddJsonBody(model);
                }
                IRestResponse response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    data.Message.Add(new MessageResult { Message = response.StatusCode.ToString() });
                }

                if (response.Content != null)
                {
                    var result = response.Content.ToString();
                    var list = JsonConvert.DeserializeObject<T>(result);
                    data.IsSuccess = true;
                    data.Result = list;
                }
                return data;
            }
            catch (System.Exception ex)
            {
                data.Message.Add(new MessageResult { Message = "No se pudo realizar la petición" + ex });
            }
            return data;
        }

        public static Response<object> Post<T>(string SiteBase, string baseAPI, string action, string token, dynamic model, string username, string password)
        {
            Response<object> data = new Response<object>
            {
                IsSuccess = false,

            };
            try
            {
                var client = new RestClient(SiteBase);
                client.Authenticator = new HttpBasicAuthenticator(username, password);
                string uri = string.Format("{0}/{1}", baseAPI, action);
                var request = new RestRequest(uri, Method.POST);
                if (token != null)
                {
                    request.AddHeader("Token", token);
                }
                if (model != null)
                {
                    // request.AddJsonBody(model);

                    var json = JsonConvert.SerializeObject(model);
                    request.AddParameter("application/json", json, ParameterType.RequestBody);
                }
                IRestResponse response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    data.IsSuccess = false;
                    data.Message.Add(new MessageResult { Message = response.StatusCode.ToString() });
                }

                if (response.Content != null)
                {
                    var result = response.Content.ToString();
                    var list = JsonConvert.DeserializeObject<T>(result);
                    data.IsSuccess = true;
                    data.Result = list;
                }
            }
            catch (System.Exception ex)
            {
                data.Message.Add(new MessageResult { Message = "No se pudo realizar la petición" + ex });
            }
            return data;
        }
    }
}
