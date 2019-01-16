using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Map : MonoBehaviour {

	private Camera mapCamera;

	public GameObject map;
	

	public List < RawImage > Companions;

	public List < RawImage > Shops;

	public List < RawImage > Enemies;

	public List < RawImage > All;

	public List < RawImage > Player;

	public List < RawImage > NPC;

    public List<RawImage> Destination;

	public Texture selectedImage;

	public Texture notSelectedImage;

	

	private bool active;

	public enum Type {
		enemy, companion, shop, destination, NPC, player
		

	}

	public void Add( Type type, RawImage icon ) {
			Assert.IsNotNull(icon, type.ToString() + " was null ");
		switch ( type ){
            case Type.companion:
				Companions.Add(icon);

	            break;
            case Type.destination:
				Destination.Add(icon);

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
            case Type.player:
				Player.Add(icon);
				break;
		}
		All.Add(icon);
	}
    public void Enable(string type) {
	    List < RawImage > list = new List < RawImage >();
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

	    list.RemoveAll( item => item == null );
	    if (list != null && list.Count > 0 ){
            active = !list[0].gameObject.activeSelf;
            foreach (var l_gameObject in list)
            {
                l_gameObject.gameObject.SetActive(active);
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
			l_gameObject.gameObject.SetActive(true);
		}
	}
	// Use this for initialization
	void Start () {
		map.SetActive(false);
	}

	public void ShowMap( ) {
		map.SetActive(true);
		//EnableAll();
	}

	public void CloseMap( ) {
		map.SetActive(false);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
