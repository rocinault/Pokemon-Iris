using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

namespace Iris
{
    public static class GameObjectUtility
    {
        public static T CreateGameobjectWithHideFlagsOnCanvas<T>(RectTransform sibling) where T : Graphic
        {
            var transform = EditorUtility.CreateGameObjectWithHideFlags(typeof(T).Name, HideFlags.HideAndDontSave, typeof(RectTransform), typeof(T)).GetComponent<RectTransform>();

            transform.SetParent(sibling.parent);

            transform.anchorMin = sibling.anchorMin;
            transform.anchorMax = sibling.anchorMax;

            transform.pivot = sibling.pivot;

            transform.position = sibling.position;
            transform.localScale = sibling.localScale;
            transform.sizeDelta = sibling.sizeDelta;

            return transform.gameObject.GetComponent<T>();
        }

        public static T CreateGameobjectWithHideFlagsOnCanvas<T>(GameObject original, RectTransform sibling)
        {
            var transform = Object.Instantiate(original).GetComponent<RectTransform>();

            transform.SetParent(sibling.parent);

            transform.anchorMin = sibling.anchorMin;
            transform.anchorMax = sibling.anchorMax;

            transform.pivot = sibling.pivot;

            transform.position = sibling.position;
            transform.localScale = sibling.localScale;
            transform.sizeDelta = sibling.sizeDelta;

            return transform.gameObject.GetComponent<T>();
        }
    }
}
