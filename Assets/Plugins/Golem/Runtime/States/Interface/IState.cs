using System;

namespace Golem
{
    public interface IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        T UniqueId { get; }

        void Enter();
        void Update();
        void FixedUpdate();
        void Exit();
    }
}
