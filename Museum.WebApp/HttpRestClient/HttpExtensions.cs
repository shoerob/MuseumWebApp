using System;
using System.Web;

namespace HttpRestClient
{
    public static class HttpExtensions
    {
        /// <summary>
        /// Uri Exstension method for appending query parameters
        /// </summary>
        /// <param name="uri">Original uri</param>
        /// <param name="name">new Query param name</param>
        /// <param name="value">new Query param value</param>
        /// <returns>Uri with new query params appended</returns>
        public static Uri AddQueryParam(this Uri uri, string name, string value)
        {
            var builder = new UriBuilder(uri);

            //decodes current query in uri
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);
            
            httpValueCollection.Add(name, value);
            
            //urlencodes the HttpValueCollection
            builder.Query = httpValueCollection.ToString();
            
            return builder.Uri;

        }
    }
}
