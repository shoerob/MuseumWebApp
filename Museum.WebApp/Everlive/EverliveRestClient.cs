using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using HttpRestClient;

namespace EverLive
{
    public class EverliveRestClient : IEverliveRestClient
    {
        private IHttpRestClient _httpRestClient;
        private string _serviceHost =  ConfigurationManager.ConnectionStrings["EverliveService"].ConnectionString;
        private string _apiKey = ConfigurationManager.AppSettings["EverliveApiKey"];
        private string _masterKey = ConfigurationManager.AppSettings["EverliveMasterKey"];
        private const string _filterHeader = "X-Everlive-Filter";
        private const string _sortHeader = "X-Everlive-Sort";
        private const string _skipHeader = "X-Everlive-Skip";
        private const string _takeHeader = "X-Everlive-Take";
        private const string _fieldsHeader = "X-Everlive-Fields";
        private const string _powerfieldsHeader = "X-Everlive-Power-Fields";
        private Uri _baseUrl;

        public EverliveRestClient(IHttpRestClient httpRestClient)
        {
            _httpRestClient = httpRestClient;
            _baseUrl = new Uri(string.Format("{0}/{1}/", _serviceHost, _apiKey));
        }

        private EverliveRequestOptions GetDefaultOptions()
        {
            var options = new EverliveRequestOptions();
            options.Headers.Add(new KeyValuePair<string, string>("Accept", "application/json;charset=utf-8"));
            options.Headers.Add(new KeyValuePair<string,string>("Accept-Charset","utf-8"));
            options.Headers.Add(new KeyValuePair<string, string>("Authorization", _masterKey));
            return options;
        }

        public async Task<int> Count(string resource)
        {
            var options = GetDefaultOptions();
            var count = await _httpRestClient.ExecuteAsync<CountResult>(HttpMethod.Get, _baseUrl, resource + "/_count", options);
            return count.Result;
        }

        public async Task<IEnumerable<T>> All<T>(string resource, string filter = "", string sort = "", int take = 0, string fields = "", string powerFields = "")
        {
            var options = GetDefaultOptions();
            if(!string.IsNullOrEmpty(filter))
            {
                options.Headers.Add(new KeyValuePair<string, string>(_filterHeader, filter));
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                options.Headers.Add(new KeyValuePair<string, string>(_sortHeader, sort));
            }
            if (take > 0)
            {
                options.Headers.Add(new KeyValuePair<string, string>(_takeHeader, take.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(fields))
            {
                options.Headers.Add(new KeyValuePair<string, string>(_fieldsHeader, fields));
            }
            if (!string.IsNullOrWhiteSpace(powerFields))
            {
                options.Headers.Add(new KeyValuePair<string, string>(_powerfieldsHeader, powerFields));
            }
            var items = await _httpRestClient.ExecuteAsync<ItemsResult<T>>(HttpMethod.Get, _baseUrl, resource, options);
            return items.Result;
        }

        public async Task<T> GetById<T>(string resource, string id)
        {
            var options = GetDefaultOptions();
            options.Id = id;

            var item = await _httpRestClient.ExecuteAsync<ItemResult<T>>(HttpMethod.Get, _baseUrl, resource, options);
            return item.Result;
        }

        public async Task<Guid> Create<T>(string resource, T newItem)
        {
            var options = GetDefaultOptions();
            options.Content = JsonConvert.SerializeObject(newItem, Formatting.None);

            var result = await _httpRestClient.ExecuteAsync<CreatedResult>(HttpMethod.Post, _baseUrl, resource, options);
            return Guid.Parse(result.Result.Id);
        }

        public async Task<IEnumerable<Guid>> Create<T>(string resource, IEnumerable<T> newItems)
        {
            var options = GetDefaultOptions();
            options.Content = JsonConvert.SerializeObject(newItems, Formatting.None);

            var result = await _httpRestClient.ExecuteAsync<CreatedResults>(HttpMethod.Post, _baseUrl, resource, options);
            List<Guid> createdIds = new List<Guid>();
            foreach (var createdItem in result.Result)
            {
                createdIds.Add(Guid.Parse(createdItem.Id));
            }
            return createdIds;
        }

        public async Task<bool> Update<T>(string resource, string id, T updatedItem)
        {
            var options = GetDefaultOptions();
            options.Id = id;
            options.Content = JsonConvert.SerializeObject(updatedItem);
  
            var result = await _httpRestClient.ExecuteAsync<UpdateResult>(HttpMethod.Put, _baseUrl, resource, options);            
            return result.Success;
        }

        public async Task<bool> Delete<T>(string resource, string id)
        {
            var options = GetDefaultOptions();
            options.Id = id;

            var result = await _httpRestClient.ExecuteAsync<DeleteResult>(HttpMethod.Delete, _baseUrl, resource, options);
            return result.Success;
        }
    }
}
