using System;

using UnityEngine;

namespace Golem
{
    public abstract class StateBehaviour<T> : MonoBehaviour, IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        public abstract T UniqueId { get; }

        public virtual void Enter()
        {
            gameObject.SetActive(true);
        }

        void IState<T>.Update()
        {
            throw new NotImplementedException();
        }

        void IState<T>.FixedUpdate()
        {
            throw new NotImplementedException();
        }

        public virtual void Exit()
        {
            gameObject.SetActive(false);
        }
    }
}
