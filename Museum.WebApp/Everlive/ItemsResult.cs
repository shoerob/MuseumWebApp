using System.Collections.Generic;

namespace EverLive
{
    internal class ItemsResult<T>
    {
        public int Count { get; set; }
        public IEnumerable<T> Result { get; set; }
    }
}
