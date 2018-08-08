using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Inventory;

public class Inventory : MonoBehaviour
{
    public const int MAX_INVENTORY_SPACE = 24;
    public int numItems;
    List<ItemBase> Items = new List<ItemBase>();
    //SortedDictionary<int id, ItemBase> UsedSpace = new SortedDictionary<int, ItemBase>();

    bool AddItem(ItemBase item)
    {
        bool added = false;

        if (HasSpace())
        {
            Items.Add(item);
            numItems++;
            added = true;
        }

        return added;
    }

    ItemBase GetItem(string itemName) //'possibly have it take in the id as well'
    {
        return Items.Where(i => i._itemName == itemName) as ItemBase;
    }

    ItemBase DropItem(string itemName)
    {
        var item = GetItem(itemName);
        Items.Remove(item);
        numItems--;

        return item;
    }

    bool HasSpace()
    {
        return numItems < MAX_INVENTORY_SPACE;
    }
}