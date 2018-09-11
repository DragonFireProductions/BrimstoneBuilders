using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    DayNightCycle dayRef;

    PlayerController player;

	// Use this for initialization
	void Start ()
    {
        dayRef = GameObject.FindGameObjectWithTag("Sun").GetComponent<DayNightCycle>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    public void SetDay()
    {
        dayRef.SetMeridiem(DayNightCycle.Meridiem.AM);
        dayRef.SetHour(8);
        dayRef.SetMinute(0);
    }

    public void SetNight()
    {
        dayRef.SetMeridiem(DayNightCycle.Meridiem.PM);
        dayRef.SetHour(8);
        dayRef.SetMinute(0);
    }

    public void DamagePlayer()
    {
        //player.health.CurValue -= 10;
    }
}
