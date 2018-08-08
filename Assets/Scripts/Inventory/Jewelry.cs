using UnityEngine;

namespace Assets.Inventory
{
    public class Jewelry : ItemBase
    {
        public int bonus_amount;

        SPECIAL_EFFECT _specialEffect;

        public Jewelry(string itemName, SPECIAL_EFFECT specialEffect = SPECIAL_EFFECT.NONE, ITEM_RARITY rarity = ITEM_RARITY.NORMAL, ITEM_SLOT itemSlot = ITEM_SLOT.JEWELERY, int id = -1) : base(itemName, rarity, id, itemSlot)
        {
            this.SpecialEffect = specialEffect;

            switch (rarity)
            {
                case ITEM_RARITY.NORMAL:
                    bonus_amount = 1;
                    break;
                case ITEM_RARITY.MAGIC:
                    bonus_amount = 5;
                    break;
                case ITEM_RARITY.UNIQUE:
                    bonus_amount = 10;
                    break;
                case ITEM_RARITY.SET:
                    bonus_amount = 8;
                    break;
                case ITEM_RARITY.QUEST:
                    bonus_amount = 0;
                    break;
                default:
                    Debug.Log("Jewelry rarity not set");
                    break;
            }

            if(specialEffect == SPECIAL_EFFECT.MANA)
            {
                bonus_amount *= 10;
            }

        }

        public SPECIAL_EFFECT SpecialEffect
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
}