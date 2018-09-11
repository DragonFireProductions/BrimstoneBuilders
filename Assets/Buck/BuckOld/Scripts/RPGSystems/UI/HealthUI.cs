﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    float visibleTime = 5f;

    float lastMadeVisibleTime;
    Transform ui;
    Image healthSlider;

    Transform cam;

    void Start ()
    {
        cam = Camera.main.transform;

        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(1).GetComponent<Image>();
                ui.gameObject.SetActive(false);

                break;
            }
        }
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
	}

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastMadeVisibleTime = Time.time;

            float healthPercent = currentHealth / (float)maxHealth;
            healthSlider.fillAmount = healthPercent;

            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }
	
	void LateUpdate ()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if (Time.time - lastMadeVisibleTime > visibleTime)
            {
                ui.gameObject.SetActive(false);
            }
        }
	}
}
