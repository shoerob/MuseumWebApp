using System.Collections.Generic;

namespace HttpRestClient
{
    /// <summary>
    /// Options to apply to an HttpRequest
    /// </summary>
    public class RequestOptions
    {
        /// <summary>
        /// List of Headers to apply to request
        /// </summary>
        public List<KeyValuePair<string, string>> Headers { get; set; }
        /// <summary>
        /// resource id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Body Content for Posts/Puts
        /// </summary>
        public string Content { get; set; }

        public string Authorization { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RequestOptions()
        {
            Headers = new List<KeyValuePair<string, string>>();
            Authorization = "BASIC";
        }
    }
}
