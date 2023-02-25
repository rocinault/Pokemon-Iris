using System;

namespace Arbok
{
    [Serializable]
    public sealed class SerializableItemSpec
    {
        public ScriptableItem asset;

        public int count;

        private SerializableItemSpec(ScriptableItem asset, int count)
        {
            this.asset = asset;
            this.count = count;
        }

        public static SerializableItemSpec Create(ItemSpec item)
        {
            return new SerializableItemSpec(item.asset, item.count);
        }
    }
}
