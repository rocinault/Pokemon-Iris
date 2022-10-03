using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Golem;

namespace Iris
{
    internal sealed class GameCoordinator : Singleton<GameCoordinator>
    {
        internal GameOverworldStateBehaviour overworldState => m_OverworldState;
        internal GameBattleStateBehaviour battleState => m_BattleState;
        internal GameMenuStateBehaviour menuState => m_MenuState;

        private GameOverworldStateBehaviour m_OverworldState;
        private GameBattleStateBehaviour m_BattleState;
        private GameMenuStateBehaviour m_MenuState;

        private static readonly Stack<IState<GameMode>> s_States = new Stack<IState<GameMode>>();

        private static bool s_IsTransitioning = false;

        protected override void Awake()
        {
            base.Awake();

            GetStartupGameModeStates();
        }

        private void GetStartupGameModeStates()
        {
            m_OverworldState = GetComponent<GameOverworldStateBehaviour>();
            m_BattleState = GetComponent<GameBattleStateBehaviour>();
            m_MenuState = GetComponent<GameMenuStateBehaviour>();
        }

        private void Start()
        {
            EnterGameMode(m_OverworldState);
        }

        internal void EnterGameMode(IState<GameMode> stateToTransitionInto)
        {
            if (!s_IsTransitioning)
            {
                StartCoroutine(TransitionGameMode(stateToTransitionInto));
            }
        }

        internal void ExitGameMode()
        {
            if (!s_IsTransitioning)
            {
                StartCoroutine(TransitionGameMode(null));
            }
        }

        private IEnumerator TransitionGameMode(IState<GameMode> stateToTransitionInto)
        {
            s_IsTransitioning = true;

            if (s_States.Count > 0)
            {
                yield return CameraFade.FadeIn();

                s_States.Peek().Exit();

                yield return SceneSystem.instance.UnloadSceneAsync(s_States.Peek().uniqueID.ToString());
            }

            if (stateToTransitionInto != null)
            {
                s_States.Push(stateToTransitionInto);
            }
            else
            {
                s_States.Pop();
            }

            yield return SceneSystem.instance.LoadSceneAsync(s_States.Peek().uniqueID.ToString());

            s_States.Peek().Enter();

            yield return CameraFade.FadeOut();

            s_IsTransitioning = false;
        }
    }
}
