using UnityEngine;
using Golem;

namespace Iris
{
	internal enum LeafState
	{
		Idle,
		Walk
	}

	internal sealed class Leaf : MonoBehaviour
	{
		private readonly StateMachine<LeafState> m_StateMachine = new StateMachine<LeafState>();

		private void Awake()
		{
			CreateStartupStates();
		}

		private void CreateStartupStates()
		{
			m_StateMachine.AddStatesToStateMachine(GetComponentsInChildren<IState<LeafState>>(true));
		}

		private void Start()
		{
			SetStateMachineID();
		}

		private void SetStateMachineID()
		{
			m_StateMachine.SetCurrentStateID(LeafState.Idle);
			m_StateMachine.Start();
		}

		internal void ChangeState(LeafState stateToTransitionInto)
		{
			Debug.Log($"changing state to {stateToTransitionInto}");
			m_StateMachine.ChangeState(stateToTransitionInto);
		}
    }
}
