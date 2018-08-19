using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "WeaponList", menuName = "Inventory/WeaponList", order = 2)]
public class WeaponList : ScriptableObject {

    public List<Weapons> weaponList;
}
