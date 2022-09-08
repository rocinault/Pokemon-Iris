using UnityEngine;

namespace Golem
{
    public abstract class YieldCoroutine : CustomYieldInstruction
    {
        public YieldCoroutine()
        {

        }

        protected virtual void Initialise()
        {

        }

        protected abstract bool Update();

        public YieldCoroutine Run()
        {
            Initialise();
            return this;
        }
    }
}