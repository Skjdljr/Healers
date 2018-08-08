using UnityEngine;

namespace Assets.Inventory
{
    public enum ARMOR_CLASS { CLOTH, LEATHER, PLATE, SHIELD }

    public class Armor : ItemBase
    {
        public int Armor_Min { get; set; }
        public int Armor_Max { get; set; }

        //consts... todo: parse from file
        private const int CLOTH_ARMOR_MIN = 1;
        private const int CLOTH_ARMOR_MAX = 10;
        private const int LEATHER_ARMOR_MIN = 5;
        private const int LEATHER_ARMOR_MAX = 20;
        private const int PLATE_ARMOR_MIN = 10;
        private const int PLATE_ARMOR_MAX = 30;
        private const int SHIELD_ARMOR_MIN = 5;
        private const int SHIELD_ARMOR_MAX = 30;

        public Armor(string itemName, ARMOR_CLASS armorClass, ITEM_SLOT itemSlot, ITEM_RARITY rarity = ITEM_RARITY.NORMAL, int id = -1) : base(itemName, rarity, id, itemSlot)
        {
            switch (armorClass)
            {
                case ARMOR_CLASS.CLOTH:
                    this.Armor_Min = CLOTH_ARMOR_MIN;
                    this.Armor_Max = CLOTH_ARMOR_MAX;
                    break;
                case ARMOR_CLASS.LEATHER:
                    this.Armor_Min = LEATHER_ARMOR_MIN;
                    this.Armor_Max = LEATHER_ARMOR_MAX;
                    break;
                case ARMOR_CLASS.PLATE:
                    this.Armor_Min = PLATE_ARMOR_MIN;
                    this.Armor_Max = PLATE_ARMOR_MAX;
                    break;
                case ARMOR_CLASS.SHIELD:
                    break;
                default:
                    Debug.Log("Armor class not set");
                    break;
            }
        }
    }
}