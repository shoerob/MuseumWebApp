using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EverLive
{
    public interface IEverliveRestClient
    {
        Task<int> Count(string resource);

        Task<IEnumerable<T>> All<T>(string resource, string filter = "", string sort = "", int take = 0, string fields = "", string powerFields = "");

        Task<T> GetById<T>(string resource, string id);

        Task<Guid> Create<T>(string resource, T newItem);

        Task<IEnumerable<Guid>> Create<T>(string resource, IEnumerable<T> newItems);

        Task<bool> Update<T>(string resource, string id, T updatedItem);

        Task<bool> Delete<T>(string resource, string id);
    }
}