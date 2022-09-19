using System;

namespace Umbreon
{
    public enum AttributeType
    {
        Health,
        Attack,
        Defence,
        Speed
    }

    public class Attribute
    {
        public float value
        {
            get => m_Value;
            set => m_Value = value;
        }

        public float maxValue
        {
            get => m_MaxValue;
            set => m_MaxValue = value;
        }

        private float m_Value;
        private float m_MaxValue;

        public Attribute(float value)
        {
            m_Value = value;
            m_MaxValue = value;
        }
    }
}
