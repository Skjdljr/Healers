public class Armor : ItemBase
{
    int Armor_Min { get; set; }
    int Armor_Max { get; set; }

    public Armor(string itemName, ITEM_RARITY rarity, int id, int itemSlot, int armor_Min, int armor_Max) : base(itemName, rarity, id, itemSlot)
    {
        this.Armor_Min = armor_Min;
        this.Armor_Max = armor_Max;
    }
}