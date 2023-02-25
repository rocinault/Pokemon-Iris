using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vulpix
{
    public sealed class SaveManager
    {
        private static LocalDataStorage s_LocalDataStorage = new LocalDataStorage();

        public static T LoadDataInternal<T>(string key)
        {
            return s_LocalDataStorage.Load<T>(key);
        }

        public static void SaveDataInternal<T>(string key, T item)
        {
            s_LocalDataStorage.Save<T>(key, item);
        }
    }
}