using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] string ItemName = "New Item";
    [SerializeField] Sprite Icon = null;
    [SerializeField] int StackSize = 64;

    public string GetName() { return ItemName; }

    public int GetStackSize() { return StackSize; }
}
