using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {

	private Camera mapCamera;

	public GameObject map;
	

	public List < GameObject > Companions;

	public List < GameObject > Shops;

	public List < GameObject > Enemies;

	public List < GameObject > All;

	public List < GameObject > Player;

	public List < GameObject > NPC;

    public List<GameObject> Destination;

	public Texture selectedImage;

	public Texture notSelectedImage;

	

	private bool active;

	public enum Type {
		enemy, companion, shop, destination, NPC
		

	}

	public void Add( Type type, GameObject icon ) {
		switch ( type ){
            case Type.companion:
				Companions.Add(icon);

	            break;
            case Type.destination:
				Companions.Add(icon);

	            break;
            case Type.enemy:
				Enemies.Add(icon);

	            break;
            case Type.shop:
				Shops.Add(icon);
				break;
            case Type.NPC:
				NPC.Add(icon);

	            break;
		}
		All.Add(icon);
	}
    public void Enable(string type) {
	    List < GameObject > list = new List < GameObject >();
        switch (type)
        {
            case "companion":
	            list = Companions;

                break;
            case "destination":
	            list = Destination;

                break;
            case "enemy":
	            list = Enemies;

                break;
            case "shop":
	            list = Shops;
                break;
            case "player":
	            list = Player;

				break;
            case "npc":
	            list = NPC;
	            break;
        }

	    if (list != null && list.Count > 0 ){
            active = !list[0].activeSelf;
            foreach (var l_gameObject in list)
            {
                l_gameObject.SetActive(active);
            }

		    
        }
	   
    }

	public void SetImage( RawImage image ) {
        if (active)
        {
            image.texture = selectedImage;
        }
        else
        {
            image.texture = notSelectedImage;
        }
    }
    public void EnableAll( ) {
		foreach ( var l_gameObject in All ){
			l_gameObject.SetActive(true);
		}
	}
	// Use this for initialization
	void Start () {
		map.SetActive(false);
	}

	public void ShowMap( ) {
		map.SetActive(true);
		EnableAll();
	}

	public void CloseMap( ) {
		map.SetActive(false);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
