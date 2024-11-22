using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaSoftware.Tests
{
    public class MemoryCache : IMemoryCache
    {
        private readonly Dictionary<string, object> _storage = new();

        public bool TryGetValue(string key, out object value)
        {
            return _storage.TryGetValue(key, out value);
        }

        public ICacheEntry CreateEntry(object key)
        {
            // Optional: if you need to simulate cache entry creation
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            _storage.Remove((string)key);
        }

        public void Set(string key, object value, TimeSpan absoluteExpirationRelativeToNow)
        {
            _storage[key] = value;
        }

        public bool TryGetValue(object key, out object? value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
