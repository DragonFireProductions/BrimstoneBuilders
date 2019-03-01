using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunChanges : MonoBehaviour
{
    [SerializeField] private Light GameSun;

    [SerializeField] private bool TurnTo_Dark;
    [SerializeField] private bool TurnTo_Light;

    public Material DaySkybox;
    public Material NightSkybox;

    private Color CurrentColor;

    private Color Yellow;
    private Color Blue;

    public bool DesertFlameQuestLink;
    private bool HitOnce;

    //Color_Yellow = FFF4D6;
    //Color_Blue = 0096B8;

    private void Start()
    {
        Yellow.r = 1;
        Yellow.g = 0.9568f;
        Yellow.b = 0.8392f;
        Yellow.a = 1;

        Blue.r = 0.0638f;
        Blue.g = 0.2803f;
        Blue.b = 0.3301f;
        Blue.a = 1;

        DesertFlameQuestLink = false;
        HitOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentColor = GameSun.color;

        if(DesertFlameQuestLink && HitOnce == false)
        {
            DesertQuestChange();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (TurnTo_Dark)
            {
                StartCoroutine(ColorLerp(CurrentColor, Blue));
                RenderSettings.skybox = NightSkybox;
            }

            if (TurnTo_Light)
            {
                StartCoroutine(ColorLerp(CurrentColor, Yellow));
                RenderSettings.skybox = DaySkybox;
            }
        }
    }

    IEnumerator ColorLerp(Color StartColor, Color EndColor)
    {
        float _time = 0;

        while (CurrentColor != EndColor)
        {
            GameSun.color = Color.Lerp(StartColor, EndColor, _time);
            _time += Time.deltaTime / 5;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator LerpColor()
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = 0.5f / 5.0f; //The amount of change to apply.
        while (progress < 1)
        {
            var currentColor = Color.Lerp(Yellow, Blue, progress);
            progress += increment;
            GameSun.color = CurrentColor;
            yield return new WaitForSeconds(1);
        }
    }

    public void DesertQuestChange()
    {
        StartCoroutine(ColorLerp(CurrentColor, Blue));
        RenderSettings.skybox = NightSkybox;
        HitOnce = true;
    }
}
