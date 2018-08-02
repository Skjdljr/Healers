public class Jewelry : ItemBase
{
    int _specialEffect;

    public Jewelry(ITEM_RARITY rarity, int id, int itemSlot, string itemName, int specialEffect) : base( rarity,  id,  itemSlot,  itemName)
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