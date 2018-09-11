using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//
// Documentation
//
//
//
//
//
//      NormailizeAmounts
//          value = current stat amount
//          inMin = minimum amount the stat can have
//          inMax = the maximum the stat can have
//          outMin = sets the inMin to a value of 0
//          outMax = stes the inMax to a value of 0
//
//
//
//
//

public class BarManger : MonoBehaviour
{
    float fillAmount;

    [SerializeField]
    float lerpSpeed;

    [SerializeField]
    Image barContent;

    [SerializeField]
    TextMeshProUGUI valueText;

    [SerializeField]
    Color startColor, endColor;

    [SerializeField]
    bool lerpColors;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ":" + value;
            fillAmount = NormalizeAmounts(value, 0, MaxValue, 0, 1);
        }
    }

	// Use this for initialization
	void Start ()
    {
        if (lerpColors)
        {
            barContent.color = startColor;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        ManageBar();
	}

    void ManageBar()
    {
        if (fillAmount != barContent.fillAmount)
        {
            barContent.fillAmount = Mathf.Lerp(barContent.fillAmount, fillAmount, lerpSpeed * Time.deltaTime);
        }

        if (lerpColors)
        {
            barContent.color = Color.Lerp(endColor, startColor, fillAmount);
        }
    }

    float NormalizeAmounts(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return(value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        //Example: (100 - 0) * (1 - 0) / (100 - 0) + 0 = 100/100 = 1 
    }
}
