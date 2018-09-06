namespace Assets.Inventory
{
    public enum WEAPON_CLASS { ONE_HANDED, TWO_HANDED }
    public enum WEAPON_TYPE { MACE, STAFF }

    public class Weapon : ItemBase
    {
        //possible buffs, casting speed, +healing, +regen, +maxMana

        public WEAPON_TYPE weaponType;
        public WEAPON_CLASS weaponClass;

        public Weapon(string itemName, WEAPON_CLASS wClass, WEAPON_TYPE wType, ITEM_RARITY rarity = ITEM_RARITY.NORMAL, int id = -1, ITEM_SLOT itemSlot = ITEM_SLOT.WEAPON) : base(itemName, rarity, id, itemSlot)
        {
            _itemName = itemName;
            weaponClass = wClass;
            weaponType = wType;
        }
    }
}