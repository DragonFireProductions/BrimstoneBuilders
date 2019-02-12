using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.Video;

public class Map : MonoBehaviour
{

    public Camera mapCamera;
    public Camera mapCamera1;
    public Camera mapCamera2;
    public GameObject map;

    public bool UseCamera = false;
    public bool UseCamera1 = false;
    public bool UseCamera2 = false;

    public List<RawImage> Companions;

    public List<RawImage> Shops;

    public List<RawImage> Enemies;

    public List<RawImage> All;

    public List<RawImage> Player;

    public List<RawImage> NPC;

    public List<RawImage> Destination;

    public Texture selectedImage;

    public Texture notSelectedImage;



    private bool active;

    public enum Type
    {
        enemy, companion, shop, destination, NPC, player


    }

    public void Add(Type type, RawImage icon)
    {
        Assert.IsNotNull(icon, type.ToString() + " was null ");
        switch (type)
        {
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
                //Shops.Add(icon);
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
    public void Enable(string type)
    {
        List<RawImage> list = new List<RawImage>();
        switch (type)
        {
            case "companion":

                foreach (var l_realTimeCompanion in StaticManager.RealTime.Companions)
                {
                    active = !l_realTimeCompanion.icon.gameObject.activeSelf;
                    l_realTimeCompanion.icon.gameObject.SetActive(active);
                }

                break;
            case "destination":

                break;
            case "enemy":

                foreach (var l_realTimeEnemy in StaticManager.RealTime.AllEnemies)
                {
                    active = !l_realTimeEnemy.icon.gameObject.activeSelf;
                    l_realTimeEnemy.icon.gameObject.SetActive(active);
                }

                break;
            case "shop":
                foreach (var l_currencyManagerShop in StaticManager.currencyManager.shops)
                {
                    active = !l_currencyManagerShop.GetComponent<Shop>().icon.activeSelf;
                    l_currencyManagerShop.GetComponent<Shop>().icon.SetActive(!l_currencyManagerShop.GetComponent<Shop>().icon.activeSelf);
                }
                break;
            case "player":
                active = !StaticManager.Character.icon.gameObject.activeSelf;
                StaticManager.Character.icon.gameObject.SetActive(active);

                break;
            case "npc":

                foreach (var l_questManagerNpC in StaticManager.questManager.NPCs)
                {
                    active = !l_questManagerNpC.GetComponent<NPC>().icon.gameObject.activeSelf;
                    l_questManagerNpC.GetComponent<NPC>().icon.gameObject.SetActive(active);
                }
                break;
        }

    }

    public void SetImage(RawImage image)
    {
        if (active)
        {
            image.texture = selectedImage;
        }
        else
        {
            image.texture = notSelectedImage;
        }
    }
    public void EnableAll()
    {
        foreach (var l_gameObject in All)
        {
            l_gameObject.gameObject.SetActive(true);
        }
    }
    // Use this for initialization
    void Start()
    {
        map.SetActive(false);
    }

    public void ShowMap()
    {
        List<GameObject> windows = new List<GameObject>();

        if (UseCamera)
        {
            map.SetActive(true);
            mapCamera.gameObject.SetActive(true);

            windows.Add(map);
            windows.Add(mapCamera.gameObject);
            windows.Add(StaticManager.uiManager.playerUI.gameObject);
        }
        else if(UseCamera1)
        {
            map.SetActive(true);
            mapCamera1.gameObject.SetActive(true);

            windows.Add(map);
            windows.Add(mapCamera1.gameObject);
            windows.Add(StaticManager.uiManager.playerUI.gameObject);
        }
        else if (UseCamera2)
        {
            map.SetActive(true);
            mapCamera2.gameObject.SetActive(true);

            windows.Add(map);
            windows.Add(mapCamera2.gameObject);
            windows.Add(StaticManager.uiManager.playerUI.gameObject);
        }

        StaticManager.UiInventory.ShowWindow(windows);
        //EnableAll();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
