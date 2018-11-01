using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableObject : MonoBehaviour
{
    [SerializeField] bool Interactable;
    [SerializeField] Transform DropPoint;
    [SerializeField] Drop[] DropTable;

    private void Update()
    {
        if (Interactable)
        {


            if (Input.GetButtonDown("Interact"))
            {
                float Distance = Vector3.Distance(transform.position, StaticManager.character.gameObject.transform.position);

                if (Distance <= 4)
                {
                    Drop();
                    this.enabled = false;
                }
            }
        }
    }

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