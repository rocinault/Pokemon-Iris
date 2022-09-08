using System;

namespace Golem
{
    public interface IState<T> where T : struct, IConvertible, IComparable, IFormattable
    {
        T uniqueID { get; }

        void Enter();
        void Update();
        void Exit();
    }
}
