using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    ///<remarks>Set in inspector</remarks>
    [SerializeField] string ItemName = "New Item";
    [SerializeField] Sprite Icon = null;
    [SerializeField] int StackSize = 64;

    /// <summary>
    /// Returns Item name
    /// </summary>
    /// <returns></returns>
    public string GetName() { return ItemName; }

    /// <summary>
    /// Returns max stack size
    /// </summary>
    /// <returns></returns>
    public int GetStackSize() { return StackSize; }

    /// <summary>
    /// returns Items icon
    /// </summary>
    /// <returns></returns>
    public Sprite GetIcon() { return Icon; }
}
