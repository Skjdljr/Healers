public class Armor : ItemBase
{
    int Armor_Min { get; set; }
    int Armor_Max { get; set; }

    public Armor(ITEM_RARITY rarity, int id, int itemSlot, string itemName, int armor_Min, int armor_Max) : base(rarity, id, itemSlot, itemName)
    {
        this.Armor_Min = armor_Min;
        this.Armor_Max = armor_Max;
    }
}