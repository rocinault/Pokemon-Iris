using System.Collections.Generic;

using UnityEngine;

namespace Vulpix
{
    internal sealed class LocalDataStorage
    {
        private readonly Dictionary<string, string> m_Storage = new Dictionary<string, string>();

        internal LocalDataStorage()
        {

        }

        internal T Load<T>(string key)
        {
            return JsonUtility.FromJson<T>(m_Storage[key]);
        }

        internal void Save<T>(string key, T item)
        {
            string dataJson = JsonUtility.ToJson(item, true);

            Debug.Log(dataJson);

            if (!string.IsNullOrEmpty(key))
            {
                m_Storage[key] = dataJson;
            }
        }

        internal bool ContainsKey(string key)
        {
            return m_Storage.ContainsKey(key);
        }
    }
}
