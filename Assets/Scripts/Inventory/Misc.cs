namespace Assets.Inventory
{
    public class Misc : ItemBase
    {
        private string _displayText;

        public Misc(string itemName, string displayText, ITEM_RARITY rarity = ITEM_RARITY.NORMAL, ITEM_SLOT itemSlot = ITEM_SLOT.MISC, int id = -1) : base(itemName, rarity, id, itemSlot)
        {
            DisplayText = displayText;
        }

        public string DisplayText
        {
            get
            {
                return _displayText;
            }

            set
            {
                _displayText = value;
            }
        }
    }
}