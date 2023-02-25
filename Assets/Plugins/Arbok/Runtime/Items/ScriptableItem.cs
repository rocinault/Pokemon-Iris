using UnityEngine;

namespace Arbok
{
    public abstract class ScriptableItem : ScriptableObject
    {
        [SerializeField]
        private string m_ItemName;

        [SerializeField]
        private Sprite m_Sprite;

        [SerializeField]
        private ItemType m_ItemType;

        public string itemName
        {
            get => m_ItemName;
        }

        public Sprite sprite
        {
            get => m_Sprite;
        }

        public ItemType itemType
        {
            get => m_ItemType;
        }

        public abstract ItemSpec CreateItemSpec();
    }
}
