using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Voltorb
{
    public abstract class GraphicalUserInterface : MonoBehaviour
    {
        private readonly Dictionary<string, Graphic> m_SceneGraphicReferences = new Dictionary<string, Graphic>();

        private void Awake()
        {
            BindSceneGraphicReferences();
        }

        protected abstract void BindSceneGraphicReferences();

        protected void Add<T>(T entry, string name = null) where T : Graphic
        {
            var key = name ?? typeof(T).Name;

            if (!m_SceneGraphicReferences.ContainsKey(key))
            {
                m_SceneGraphicReferences.Add(key, entry);
            }
        }

        public void Show<T>(string name = null) where T : Graphic
        {
            StartCoroutine(ShowAsync<T>(name));
        }

        public IEnumerator ShowAsync<T>(string name = null) where T : Graphic
        {
            var key = name ?? typeof(T).Name;

            if (m_SceneGraphicReferences.ContainsKey(key))
            {
                yield return m_SceneGraphicReferences[key].Show();
            }
            else
            {
                throw new KeyNotFoundException($"key for type {key} was not registered");
            }
        }

        public void ShowAll()
        {
            foreach (var graphic in m_SceneGraphicReferences.Values)
            {
                graphic.gameObject.SetActive(true);
            }
        }

        public void Hide<T>(string name = null) where T : Graphic
        {
            StartCoroutine(HideAsync<T>(name));
        }

        public IEnumerator HideAsync<T>(string name = null) where T : Graphic
        {
            var key = name ?? typeof(T).Name;

            if (m_SceneGraphicReferences.ContainsKey(key))
            {
                yield return m_SceneGraphicReferences[key].Hide();
            }
            else
            {
                throw new KeyNotFoundException($"key for type {key} was not registered");
            }
        }

        public void HideAll()
        {
            foreach (var graphic in m_SceneGraphicReferences.Values)
            {
                graphic.gameObject.SetActive(false);
            }
        }

        public void SetProperties<T>(string key, T props) where T : GraphicProperties
        {
            if (m_SceneGraphicReferences.ContainsKey(key))
            {
                if (m_SceneGraphicReferences.TryGetValue(key, out Graphic graphic))
                {
                    ((Graphic<T>)graphic).SetProperties(props);
                }
            }
            else
            {
                throw new KeyNotFoundException($"key for type {key} was not registered");
            }
        }

        public void SetActive<T>(bool value, string name = null) where T : Graphic
        {
            var key = name ?? typeof(T).Name;

            if (m_SceneGraphicReferences.ContainsKey(key))
            {
                m_SceneGraphicReferences[key].gameObject.SetActive(value);
            }
            else
            {
                throw new KeyNotFoundException($"key for type {key} was not registered");
            }
        }

        public T Get<T>(string name = null) where T : Graphic
        {
            var key = name ?? typeof(T).Name;

            if (m_SceneGraphicReferences.ContainsKey(key))
            {
                return (T)m_SceneGraphicReferences[key];
            }
            else
            {
                throw new KeyNotFoundException($"key for type {key} was not registered");
            }
        }

        private void OnDestroy()
        {
            ClearAllSceneGraphicReferences();
        }

        private void ClearAllSceneGraphicReferences()
        {
            m_SceneGraphicReferences.Clear();
        }
    }
}