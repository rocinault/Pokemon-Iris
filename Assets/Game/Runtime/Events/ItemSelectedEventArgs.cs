using Arbok;

namespace Iris
{
    internal readonly struct ItemSelectedEventArgs
    {
        internal readonly ItemSpec itemToUse;

        private ItemSelectedEventArgs(ItemSpec itemToUse)
        {
            this.itemToUse = itemToUse;
        }

        internal static ItemSelectedEventArgs Create(ItemSpec itemToUse)
        {
            return new ItemSelectedEventArgs(itemToUse);
        }
    }
}
