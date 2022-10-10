using System.Collections;

using UnityEngine;

using Umbreon;

namespace Iris
{
    [CreateAssetMenu(fileName = "ScriptableObject/Items/Pokeball", menuName = "Pokeball")]
    internal sealed class Pokeball : ScriptableItem
    {
        [SerializeField]
        private int m_CatchRate;

        public override ItemSpec CreateItemSpec()
        {
            return new PokeballSpec(this);
        }

        private sealed class PokeballSpec : ItemSpec
        {
            public PokeballSpec(ScriptableItem asset, uint count = 1) : base(asset, count)
            {

            }

            public override IEnumerator UseItem(Combatant target)
            {
                Debug.Log($"using pokeball");

                yield return null;
            }
        }
    }
}
