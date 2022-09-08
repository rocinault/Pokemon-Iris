using UnityEngine;
using UnityEditor;

namespace Golem
{
    public abstract class Singleton<TInstance> : MonoBehaviour where TInstance : Singleton<TInstance>
    {
        public static TInstance instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType<TInstance>();

                    if (s_Instance == null)
                    {
                        s_Instance = CreateHiddenGameObjectInstanceAndDontSave();
                    }
                }

                return s_Instance;
            }
        }

        private static TInstance s_Instance;

        protected virtual void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = (TInstance)this;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogError(string.Concat($"Multiple instances of singelton type {typeof(TInstance).Name}"));
            }
#endif
        }

        private static TInstance CreateHiddenGameObjectInstanceAndDontSave()
        {
            return EditorUtility.CreateGameObjectWithHideFlags(typeof(TInstance).Name, HideFlags.HideAndDontSave, typeof(TInstance)).GetComponent<TInstance>();
        }
    }
}
