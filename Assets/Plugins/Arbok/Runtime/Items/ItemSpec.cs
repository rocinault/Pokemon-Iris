using System.Collections;

//using Eevee;

namespace Arbok
{
    public abstract class ItemSpec
    {
        public readonly ScriptableItem asset;

        public int count = 1;

        protected ItemSpec(ScriptableItem asset)
        {
            this.asset = asset;
        }

        protected ItemSpec(ScriptableItem asset, int count) : this(asset)
        {
            this.count = count;
        }

        //public virtual void PreItemUse(Combatant instigaator, Combatant target)
        //{
            
        //}

        //public abstract IEnumerator UseItem(Combatant instigaator, Combatant target);

        //public virtual void PostItemUse(Combatant instigator, Combatant target)
        //{

        //}

        //protected virtual bool CanUseItem(Combatant instigator, Combatant target)
        //{
        //    if (!CheckItemCountIsGreatorThanZero())
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        private bool CheckItemCountIsGreatorThanZero()
        {
            return count > 0;
        }
    }
}
