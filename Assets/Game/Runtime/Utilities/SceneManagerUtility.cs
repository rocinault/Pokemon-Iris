using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;

namespace Iris
{
    internal static class SceneManagerUtility
    {
        private static readonly HashSet<string> s_LoadedScenes = new HashSet<string>();

        internal static IEnumerator LoadAdditiveSceneAsync(string sceneName)
        {
            if (!s_LoadedScenes.Contains(sceneName))
            {
                s_LoadedScenes.Add(sceneName);

                yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
        }

        internal static IEnumerator UnLoadAdditiveSceneAsync(string sceneName)
        {
            if (s_LoadedScenes.Contains(sceneName))
            {
                s_LoadedScenes.Remove(sceneName);

                yield return SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            }
        }
    }
}
