public class Weapon : ItemBase
{
    float _minDmgLow { get; set; }
    float _maxDmgLow { get; set; }
    float _minDmgHigh { get; set; }
    float _maxDmgHigh { get; set; }
    float _weaponSpeed { get; set; }

    public Weapon(string itemName, ITEM_RARITY rarity, int id, int itemSlot, float minDmgLow, float maxDmgLow, float minDmgHigh, float maxDmgHigh, float weaponSpeed) : base(itemName, rarity, id, itemSlot)
    {
        _minDmgLow = minDmgLow;
        _maxDmgLow = maxDmgLow;
        _minDmgHigh = minDmgHigh;
        _maxDmgHigh = maxDmgHigh;
        _weaponSpeed = weaponSpeed;
    }
}