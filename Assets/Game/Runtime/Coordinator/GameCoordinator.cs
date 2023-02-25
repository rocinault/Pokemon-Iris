using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Golem;
using Vulpix;

namespace Iris
{
    internal sealed class GameCoordinator : Singleton<GameCoordinator>
    {

    }
}

/*
         private static readonly Stack<IGameState<GameMode>> s_States = new Stack<IGameState<GameMode>>();

        private void Start()
        {
            //EnterGameMode(OverworldGameState<GameMode>.Create(GameMode.Overworld, "Overworld"));
            EnterGameMode(MenuBagGameState.Create(GameMode.Menu, "menu-bag"));
        }

        internal void EnterGameMode(IGameState<GameMode> stateToTransitionInto)
        {
            StartCoroutine(EnterGameModeAsync(stateToTransitionInto));
        }

        internal void ExitGameMode()
        {
            StartCoroutine(ExitGameModeAsync());
        }

        private IEnumerator EnterGameModeAsync(IGameState<GameMode> stateToTransitionInto)
        {
            if (s_States.Count > 0)
            {
                yield return UnloadSceneAndExitGameModeAsync();
            }

            s_States.Push(stateToTransitionInto);

            yield return LoadSceneAndEnterGameModeAsync();
        }

        private IEnumerator ExitGameModeAsync()
        {
            yield return UnloadSceneAndExitGameModeAsync();

            s_States.Pop();

            if (s_States.Count > 0)
            {
                yield return LoadSceneAndEnterGameModeAsync();
            }
        }

        private IEnumerator LoadSceneAndEnterGameModeAsync()
        {
            yield return SceneManagerUtility.LoadAdditiveSceneAsync(s_States.Peek().sceneName);

            s_States.Peek().Enter();

            yield return SaveManager.LoadAllDataInternalAsync();
        }

        private IEnumerator UnloadSceneAndExitGameModeAsync()
        {
            yield return SaveManager.SaveAllDataInternalAsync();

            s_States.Peek().Exit();

            yield return SceneManagerUtility.UnLoadAdditiveSceneAsync(s_States.Peek().sceneName);
        } 
 */ 