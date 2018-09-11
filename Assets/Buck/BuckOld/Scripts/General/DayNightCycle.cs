using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    public enum Meridiem
    {
        AM, PM
    }

    [Header("Time")]

    [SerializeField]
    [Range(1, 12)]
    int dayHour = 6;

    [SerializeField]
    [Range(0, 59)]
    int dayMinute = 0;

    [SerializeField]
    Meridiem dayMeridiem = Meridiem.AM;

    [Header("Conversion")]

    [SerializeField]
    float dayMinuteToSecond = 1.0f;

    [Header("Attributes")]

    [SerializeField]
    bool canCycleDay = true;

    [SerializeField]
    int daysPassed = 1;

    [SerializeField]
    string mpartOfDay;

    //Elapsed minutes to seconds
    float elapsedMS = 0.0f;

	// Use this for initialization
	void Start ()
    {
        AdjustMeridiem();

        TrackPartOfDay();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!canCycleDay)
        {
            return;
        }

        elapsedMS += Time.deltaTime * dayMinuteToSecond;

        if (elapsedMS > 1.0f)
        {
            elapsedMS = 0.0f;

            dayMinute += 1;

            if (dayMinute > 59)
            {
                dayMinute = 0;

                dayHour += 1;

                if (dayHour > 12)
                {
                    dayHour = 1;

                    switch (dayMeridiem)
                    {
                        case Meridiem.AM:
                            {
                                dayMeridiem = Meridiem.PM;
                                break;
                            }
                        case Meridiem.PM:
                            {
                                dayMeridiem = Meridiem.AM;
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }
                }
            }

            if (dayMeridiem == Meridiem.AM)
            {
                if (dayHour >= 6 && dayMinute == 0)
                {
                    daysPassed++;
                    return;
                }
            }

            AdjustMeridiem();

            TrackPartOfDay();
        }
	}

    //Mutators
    public void SetCycle(bool canCycle)
    {
        canCycleDay = canCycle;
    }

    public void SetHour(int hour)
    {
        dayHour = hour;

        if (dayHour > 12)
        {
            hour = 12;
        }
        else if (hour < 1)
        {
            hour = 1;
        }

        AdjustMeridiem();
    }

    public void SetMinute(int minute)
    {
        dayMinute = minute;

        if (dayMinute > 59)
        {
            dayMinute = 59;
        }
        else if (dayMinute < 0)
        {
            dayMinute = 0;
        }

        AdjustMeridiem();
    }

    public void SetMeridiem(Meridiem meridiem)
    {
        dayMeridiem= meridiem;
    }

    public void SetMinuteToSecond(float minuteToSecond)
    {
        dayMinuteToSecond = minuteToSecond;
    }

    //Accessors
    public bool CanCycle()
    {
        return canCycleDay;
    }

    public int GetHour()
    {
        return dayHour;
    }

    public int GetMinute()
    {
        return dayMinute;
    }

    public int GetDaysPassed()
    {
        return daysPassed;
    }

    public Meridiem GetMeridiem()
    {
        return dayMeridiem;
    }

    public float GetMinuteToSecond()
    {
        return dayMinuteToSecond;
    }

    public string GetPartOfDay()
    {
        return mpartOfDay;
    }

    void AdjustMeridiem()
    {
        switch (dayMeridiem)
        {
            case Meridiem.AM:
                {
                    transform.eulerAngles = new Vector3(270 + dayHour * 15 + dayMinute * 0.25f, 0, 0);
                    break;
                }
            case Meridiem.PM:
                {
                    transform.eulerAngles = new Vector3(90 + dayHour * 15 + dayMinute * 0.25f, 0, 0);
                    break;
                }

            default:
                {
                    break;
                }
        }
    }

    void TrackPartOfDay()
    {
        if (dayMeridiem == Meridiem.AM)
        {
            if (dayHour > 12 && dayHour <= 5)
            {
                mpartOfDay = "dawn";
            }

            if (dayHour > 5 && dayHour <= 6)
            {
                mpartOfDay = "early morning";
            }

            if (dayHour > 6 && dayHour <= 9)
            {
                mpartOfDay = "morning";
            }

            if (dayHour > 9 && dayHour <= 11)
            {
                mpartOfDay = "mid-morning";
            }

            if (dayHour > 11 && dayHour <= 12)
            {
                mpartOfDay = "afternoon";
            }
        }

        else if (dayMeridiem == Meridiem.PM)
        {
            if (dayHour > 12 && dayHour <= 2)
            {
                mpartOfDay = "afternoon";
            }

            if (dayHour > 2 && dayHour <= 5)
            {
                mpartOfDay = "evening";
            }

            if (dayHour > 5 && dayHour <= 8)
            {
                mpartOfDay = "dusk";
            }

            if (dayHour > 8 && dayHour <= 11)
            {
                mpartOfDay = "night";
            }

            if (dayHour > 11 && dayHour <= 12)
            {
                mpartOfDay = "mid-night";
            }
        }
    }
}
