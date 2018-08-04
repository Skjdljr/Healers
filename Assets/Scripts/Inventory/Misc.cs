public class Misc : ItemBase
{
    private string _displayText;

    public Misc(string itemName, ITEM_RARITY rarity, int id, int itemSlot, string displayText) : base(itemName, rarity, id, itemSlot)
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