using UnityEngine;

public interface IItemBase
{
    //Each item type will have its own classifications
    //for weapons  enum ITEM_CLASS 1 | 2 handed
    //for armor... Cloth,Leather,Plate
    //for jewel... Necklace, earing, ring

    //enum ITEM_TYPE { ARMOR, WEAPON, JEWELRY, MISC }
    //enum ITEM_RARITY { NORMAL, MAGIC, UNIQUE, SET, QUEST }

    //ITEM_RARITY rarity;
    //ITEM_TYPE type;

    
    int id { get; set; }
    bool isEquipped { get; set; }
    int itemSlot { get; set; }
    string itemName { get; set; }

}