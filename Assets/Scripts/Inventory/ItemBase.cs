using UnityEngine;
using rds;

namespace Assets.Inventory
{
    public enum ITEM_SLOT { CHEST_ARMOR, HELMET, SHIELD, WEAPON, JEWELERY, MISC, INVENTORY }
    public enum ITEM_RARITY { NORMAL, MAGIC, UNIQUE, SET, QUEST }

    //Should prob be added to base item constructor
    public enum SPECIAL_EFFECT { NONE, MANA, REGEN, STAT_BONUS } //?

    public class ItemBase : RDSObject
    {
        public int _id { get; set; }
        public string _itemName { get; set; }
        public ITEM_RARITY _rarity { get; set; }
        public ITEM_SLOT _itemSlot { get; set; }

        public float TakeDamage(float dmg)
        {
            return dmg;
        }

        public ItemBase(string itemName, ITEM_RARITY rarity = ITEM_RARITY.NORMAL, int id = -1, ITEM_SLOT itemSlot = ITEM_SLOT.INVENTORY)
        {
            this._rarity = rarity;
            this._id = id;
            this._itemSlot = itemSlot;
            this._itemName = itemName;
        }

        public override string ToString()
        {
            return _itemName;
        }

        public bool IsEquiped() { return this._itemSlot >= 0; }
    }
}