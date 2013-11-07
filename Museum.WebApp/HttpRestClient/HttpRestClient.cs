using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HttpRestClient
{
    public class HttpRestClient : IHttpRestClient
    {
        /// <summary>
        /// Executes an HttpRequest Asyncrhonously
        /// </summary>
        /// <typeparam name="T">Expected return type</typeparam>
        /// <param name="method">HttpRequest Method: GET, POST, PUT, DELETE</param>
        /// <param name="serviceHost">host uri</param>
        /// <param name="resource">relative resource uri</param>
        /// <param name="options">Request options</param>
        /// <returns>Task of type T</returns>
        public async Task<T> ExecuteAsync<T>(HttpMethod method, Uri serviceHost, string resource, RequestOptions options = null)
        {
            HttpClient client = new HttpClient();
            T result = default(T);

            //full uri
            var uri = new Uri(serviceHost, resource);
            //build request
            var request = BuildRequest(method, uri.ToString(), options);              
            //send request
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                //deserialize result
                result = GetResult<T>(response);
            }
            else
            {
                //pass through unsuccessful response, after clearing headers
                response.Headers.Clear();
                throw new HttpResponseException(response);
            }

            return result;
        }

        /// <summary>
        /// Constructs an HttpRequest for the given method, uri and options
        /// </summary>
        /// <param name="method">Http Method: GET, POST, PUT, DELETE, etc..</param>
        /// <param name="uri">Full uri</param>
        /// <param name="options">Request Options</param>
        /// <returns>Fully constructed HttpRequestMessage</returns>
        private HttpRequestMessage BuildRequest(HttpMethod method, string uri, RequestOptions options)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, uri);
            
            if (options != null)
            {
                //id
                if(!string.IsNullOrWhiteSpace(options.Id))
                {
                    request.RequestUri = new Uri(request.RequestUri.ToString() + "/" + options.Id);
                }
                //content
                if (!string.IsNullOrWhiteSpace(options.Content) && (method == HttpMethod.Post || method == HttpMethod.Put))
                {
                    request.Content = new StringContent(options.Content, Encoding.UTF8, "application/json");
                }
                //headers
                if (options.Headers != null)
                {
                    foreach (KeyValuePair<string, string> header in options.Headers)
                    {
                        if (header.Key == HttpRequestHeader.Authorization.ToString())
                        {
                            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(options.Authorization, header.Value);
                        }
                        else
                        {
                            request.Headers.Add(header.Key, header.Value);
                        }
                    }
                }

               
            }

            return request;
        }

        /// <summary>
        /// Gets content from HttpResponseMessage
        /// </summary>
        /// <typeparam name="T">Content type</typeparam>
        /// <param name="response">HttpResponseMesage</param>
        /// <returns>Content T</returns>
        private T GetResult<T>(HttpResponseMessage response)
        {
            var result = default(T);

            if (response != null && response.Content != null && response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }

            return result;
        }

    }
}
