using System;

using UnityEngine;

namespace Golem
{
    [CreateAssetMenu(fileName = "ScriptableObject/RuntimeSet/GameObject", menuName = "GameObjectRuntimeSet")]
    public sealed class GameObjectRuntimeSet : RuntimeSet<GameObject>
    {
        private const int kMinValueForFirstItem = 0;

        public T GetComponentFromRuntimeSet<T>() where T : MonoBehaviour
        {
            return GetFirstGameObject().GetComponent<T>();
        }

        public GameObject GetFirstGameObject()
        {
#if UNITY_EDITOR
            VerifyRuntimeSetCountIsGreaterThanZero();
#endif
            return m_Collection[kMinValueForFirstItem];
        }

        private void VerifyRuntimeSetCountIsGreaterThanZero()
        {
            if (Count() <= 0)
            {
                string message = string.Concat($"The count for gameobject runtime set {name} " +
                    $"is currently: {Count()}, amount must be at least one.");
                throw new ArgumentOutOfRangeException(message);
            }
        }
    }
}
