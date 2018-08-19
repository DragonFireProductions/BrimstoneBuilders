using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WeaponClass : MonoBehaviour {
    
    public Weapons weapon;
    private bool CanShoot = true;
    // Use this for initialization
    public void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
       
    }
   

    private void OnTriggerEnter(Collider collider)
    {
       
            Debug.Log("Object Name: " + weapon.objectName);
            Debug.Log("Weapon Value: " + weapon.value);
    }

}
