using UnityEngine;

public class ItemBase : MonoBehaviour
{
    //Can easily break these apart and put each one in its respectable class w.e
    enum ITEM_CLASS
    { /*Armor*/
        HELMET = 0, GLOVES, CHEST, PANTS, BELT, BOOTS, SHOULDERS,
        /*Weapon*/
        AXE = 7, DAGGER, MACE, SWORD, WAND,
        /*Jewelry*/
        NECKLACE = 12, EAR_RING, RING, BRACELET,
        /*Misc*/
        BOOK = 16, PAPER, SHARD, TOKEN, TRINKET
    }

    enum ITEM_TYPE
    { /*Armor*/
        CLOTH = 0, LEATHER , PLATE,
        /*Weapon*/
        ONE_HANDED = 3, TWO_HANDED,
        JEWELRY = 5,
        MISC = 6
    }

    enum ITEM_RARITY { NORMAL, MAGIC, UNIQUE, SET, QUEST }

    public int id;
    public bool isEquipped;
    public string itemName;
    ITEM_RARITY rarity;
    ITEM_CLASS item_Class;
    ITEM_TYPE type;
}