using System;
using System.Collections;

using UnityEngine;

using Golem;

namespace Iris
{
    internal sealed class BattleLostState<T> : State<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        private readonly BattleGraphicsInterface m_GraphicsInterface;
        private readonly BattleCoordinator m_Coordinator;

        public BattleLostState(T uniqueID, BattleGraphicsInterface graphicsInterface, BattleCoordinator coordinator) : base(uniqueID)
        {
            m_GraphicsInterface = graphicsInterface;
            m_Coordinator = coordinator;
        }

        public override void Enter()
        {
            m_Coordinator.StartCoroutine(WildPokemonBattleLostEndSequence());
        }

        private IEnumerator WildPokemonBattleLostEndSequence()
        {
            yield return m_GraphicsInterface.HideAsync<PlayerPokemonPanel>();
        }
    }
}
