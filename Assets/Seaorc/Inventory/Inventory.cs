using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] float Capacity;
    [SerializeField] List<InventorySlot> Items;

    /// <summary>
    /// Adds an item to inventory
    /// </summary>
    /// <param name="_item"></param>
    /// <returns></returns>
    public bool Add(Item _item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if(Items[i].GetItem() == _item)
            {
                if(Items[i].GetCount() < _item.GetStackSize())
                {
                    Items[i].IncrementCount();
                    return true;
                }
            }
        }

        if(Items.Count < Capacity)
        {
            InventorySlot NewItem = new InventorySlot();

            NewItem.SetItem(_item);
            NewItem.SetCount(1);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Removes an item at current index
    /// </summary>
    /// <param name="Index"></param>
    public void Delete(int Index)
    {
        Items.RemoveAt(Index);
    }

    public void Print(int Index)
    {
        Item item = Items[Index].GetItem();
        int Count = Items[Index].GetCount();

        Debug.Log("Name: " + item.GetName() + "   Count: " + Count);
    }

}

/// <summary>
/// basic structure containing data for indevidual inventory slots
/// </summary>
public struct InventorySlot
{
    Item SlotType;
    int Count;

    public void SetItem(Item _item) { SlotType = _item; }
    public Item GetItem() { return SlotType; }
    public void SetCount(int _count) { Count = _count; }
    public void IncrementCount(int _addition = 1) { Count += _addition; }
    public int GetCount() { return Count; }
}