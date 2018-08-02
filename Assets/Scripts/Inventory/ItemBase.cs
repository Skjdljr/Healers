using UnityEngine;

public class ItemBase : MonoBehaviour
{
    //Each item type will have its own classifications
    //for weapons  enum ITEM_CLASS 1 | 2 handed
    //for armor... Cloth,Leather,Plate
    //for jewel... Necklace, earing, ring

    //enum ITEM_TYPE { ARMOR, WEAPON, JEWELRY, MISC }
    //enum ITEM_RARITY { NORMAL, MAGIC, UNIQUE, SET, QUEST }

    //ITEM_RARITY rarity;
    //ITEM_TYPE type;

    public int id { get; set; }
    public bool isEquipped { get; set; }
    public int itemSlot { get; set; }
    public string itemName { get; set; }
}