using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Golem;

namespace Iris
{
    internal sealed class GameCoordinator : Singleton<GameCoordinator>
    {
        private static readonly Stack<IState<GameMode>> s_States = new Stack<IState<GameMode>>();

        private static GameOverworldState<GameMode> s_OverworldState;
        private static GameBattleState<GameMode> s_BattleState;

        private static bool s_IsTransitioning = false;

        protected override void Awake()
        {
            base.Awake();

            CreateStartupGameModeStates();
        }

        private void CreateStartupGameModeStates()
        {
            s_OverworldState = new GameOverworldState<GameMode>(GameMode.Overworld, instance);
            s_BattleState = new GameBattleState<GameMode>(GameMode.Battle, instance);
        }

        private void Start()
        {
            StartOverworldGameMode();
        }

        private void StartOverworldGameMode()
        {
            EnterGameMode(s_OverworldState);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EnterGameMode(s_BattleState);
            }
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

    internal sealed class GameOverworldState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly GameCoordinator m_Coordinator;

        public GameOverworldState(T uniqueID, GameCoordinator coordinator) : base(uniqueID)
        {
            m_Coordinator = coordinator;
        }
    }

    internal sealed class GameBattleState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly GameCoordinator m_Coordinator;

        public GameBattleState(T uniqueID, GameCoordinator coordinator) : base(uniqueID)
        {
            m_Coordinator = coordinator;
        }
    }
}
