public class Jewelry : ItemBase
{
    int _specialEffect;

    public Jewelry(string itemName, ITEM_RARITY rarity, int id, int itemSlot, int specialEffect) : base(itemName, rarity,  id,  itemSlot)
    {
        this.SpecialEffect = specialEffect;
    }

    public int SpecialEffect
    {
        get
        {
            return _specialEffect;
        }

        set
        {
            _specialEffect = value;
        }
    }
}