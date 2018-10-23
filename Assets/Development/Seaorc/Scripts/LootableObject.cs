using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableObject : MonoBehaviour
{
    [SerializeField] Drop[] DropTable;
    [SerializeField] Transform DropPoint;

    public void Drop(int DropCount = 1)
    {
        for (int i = 0; i < DropCount; i++)
        {
            foreach  (Drop item in DropTable)
            {
                if( Random.Range(1,100) <= item.DropChance)
                {
                    if (DropPoint != null)
                        Instantiate(item.Item, DropPoint.position, DropPoint.rotation);
                    else
                        Instantiate(item.Item, transform.position, transform.rotation);

                    break;
                }
            }
        }
    }
}

[System.Serializable]
struct Drop
{
    public GameObject Item;
    public float DropChance;
}