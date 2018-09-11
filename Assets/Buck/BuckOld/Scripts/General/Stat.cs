using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    public BarManger bar;

    [SerializeField]
    float maxValue;

    float curValue;

    public float CurValue
    {
        get
        {
            return curValue;
        }

        set
        {
            curValue = Mathf.Clamp(value, 0, MaxValue);
            bar.Value = curValue;
        }
    }

    public float MaxValue
    {
        get
        {
            return maxValue;
        }

        set
        {
            maxValue = value;
            bar.MaxValue = maxValue;
        }
    }

    public void SetValues()
    {
        MaxValue = maxValue;
        CurValue = maxValue;
    }
}
