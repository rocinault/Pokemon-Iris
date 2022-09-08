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
        public float currentValue
        {
            get => m_CurrentValue;
        }

        public float maxValue
        {
            get => m_MaxValue;
        }

        private readonly float m_CurrentValue;
        private readonly float m_MaxValue;

        public Attribute(float value)
        {
            m_CurrentValue = value;
            m_MaxValue = value;
        }
    }
}
