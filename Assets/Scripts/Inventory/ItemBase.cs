using UnityEngine;

public class ItemBase : MonoBehaviour
{
    //Each item type will have its own classifications
    //for weapons  enum ITEM_CLASS 1 | 2 handed
    //for armor... Cloth,Leather,Plate
    //for jewel... Necklace, earing, ring

    //enum ITEM_TYPE { BASE, ARMOR, WEAPON, JEWELRY, MISC }
    public enum ITEM_RARITY { NORMAL, MAGIC, UNIQUE, SET, QUEST }

    public ITEM_RARITY _rarity { get; set; }
    public int _id { get; set; }
    public int _itemSlot { get; set; }
    public string _itemName { get; set; }

    public float TakeDamage(float dmg)
    {
        return dmg;
    }

    public ItemBase(ITEM_RARITY rarity, int id, int itemSlot, string itemName)
    {
        this._rarity = rarity;
        this._id = id;
        this._itemSlot = itemSlot;
        this._itemName = itemName;
    }

    public bool IsEquiped() { return this._itemSlot >= 0; }
}