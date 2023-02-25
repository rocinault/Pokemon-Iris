using System;

using UnityEngine;

using Arbok;

namespace Iris
{
    [CreateAssetMenu(fileName = "potion", menuName = "ScriptableObjects/Iris/Items/Potion", order = 150)]
    internal sealed class Potion : ScriptableItem
    {
        public override ItemSpec CreateItemSpec()
        {
            return new PotionSpec(this);
        }

        private sealed class PotionSpec : ItemSpec
        {
            public PotionSpec(ScriptableItem asset) : base(asset)
            {

            }
        }
    }
}
