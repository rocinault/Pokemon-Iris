using UnityEngine;

namespace Umbreon
{
    public sealed class EffectSpec
    {
        private readonly ScriptableEffect m_Asset;

        public EffectSpec(ScriptableEffect asset)
        {
            m_Asset = asset;
        }

        public bool TryRemoveEffectSpec()
        {
            return true;
        }
    }
}
