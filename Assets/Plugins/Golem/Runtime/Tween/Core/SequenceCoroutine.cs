using UnityEngine;

namespace Golem
{
    public sealed class SequenceCoroutine : CustomYieldInstruction
    {
        private YieldCoroutine[] m_Collection = new YieldCoroutine[] { };

        private int m_Index;

        public override bool keepWaiting => Update();

        public SequenceCoroutine()
        {
            m_Index = -1;
        }

        private void Initialise()
        {
            m_Index++;
            m_Collection[m_Index].Run();
        }

        private bool Update()
        {
            if (!m_Collection[m_Index].MoveNext())
            {
                m_Index++;

                if (m_Index < m_Collection.Length)
                {
                    m_Collection[m_Index].Run();
                }
            }

            return IsComplete();
        }

        private bool IsComplete()
        {
            return m_Index < m_Collection.Length;
        }

        public SequenceCoroutine Run()
        {
            Initialise();
            return this;
        }

        public SequenceCoroutine Build(params YieldCoroutine[] values)
        {
            m_Collection = values;
            return this;
        }
    }
}