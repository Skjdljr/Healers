public class Misc : ItemBase
{
    private string _displayText;

    public Misc(ITEM_RARITY rarity, int id, int itemSlot, string itemName, string displayText) : base(rarity, id, itemSlot, itemName)
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