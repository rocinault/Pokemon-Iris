using System;

namespace Golem
{
    public abstract class State<T> : IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        public T UniqueId { get; }

        public State(T uniqueId)
        {
            this.UniqueId = uniqueId;
        }

        public virtual void Enter()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}
