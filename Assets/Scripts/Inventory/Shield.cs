using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Inventory
{
    public enum SHIELD_TYPE { SMALL, MEDIUM, LARGE }

    public class Shield : Armor
    {
        public float blockChanceMax;
        public float blockChanceMin;

        private const int SMALL_BLOCK_CHANCE_MIN = 10;
        private const int SMALL_BLOCK_CHANCE_MAX = 35;
        private const int MEDIUM_BLOCK_CHANCE_MIN = 15;
        private const int MEDIUM_BLOCK_CHANCE_MAX = 45;
        private const int LARGE_BLOCK_CHANCE_MIN = 20;
        private const int LARGE_BLOCK_CHANCE_MAX = 50;

        public Shield(string itemName, SHIELD_TYPE shieldType, ITEM_RARITY rarity = ITEM_RARITY.NORMAL, ITEM_SLOT itemSlot = ITEM_SLOT.SHIELD, int id = -1) : base(itemName, ARMOR_CLASS.SHIELD, itemSlot, rarity, id)
        {
            switch (shieldType)
            {
                case SHIELD_TYPE.SMALL:
                    blockChanceMin = MEDIUM_BLOCK_CHANCE_MIN;
                    blockChanceMax = SMALL_BLOCK_CHANCE_MAX;

                    Armor_Max -= 15;
                    break;
                case SHIELD_TYPE.MEDIUM:
                    blockChanceMin = MEDIUM_BLOCK_CHANCE_MIN;
                    blockChanceMax = MEDIUM_BLOCK_CHANCE_MAX;

                    Armor_Min += 5;
                    Armor_Max -= 5;
                    break;
                case SHIELD_TYPE.LARGE:
                    blockChanceMin = LARGE_BLOCK_CHANCE_MIN;
                    blockChanceMax = LARGE_BLOCK_CHANCE_MAX;

                    Armor_Min += 10;
                    Armor_Max += 5;
                    break;

                default:
                    Debug.Log("Shield type not set");
                    break;
            }
        }
    }
}