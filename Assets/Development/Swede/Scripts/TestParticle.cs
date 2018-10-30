using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{
    bool active_rain = false, active_smoke = false, active_selected = false, active_embers = false;
    ParticleSystem particle_rain = null, particle_smoke = null, particle_selected = null, particle_embers = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            active_rain = !active_rain;
            if (active_rain == true)
            {
                particle_rain = StaticManager.particleManager.Play(ParticleManager.states.Rain, StaticManager.Character.transform.position);
            }
            else
            {
                StaticManager.particleManager.Stop(particle_rain);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            active_smoke = !active_smoke;
            if (active_smoke == true)
            {
                particle_smoke = StaticManager.particleManager.Play(ParticleManager.states.Smoke, StaticManager.Character.transform.position);
            }
            else
            {
                StaticManager.particleManager.Stop(particle_smoke);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            active_selected = !active_selected;
            if (active_selected == true)
            {
                particle_selected = StaticManager.particleManager.Play(ParticleManager.states.Selected, StaticManager.Character.transform.position, StaticManager.Character.transform);
            }
            else
            {
                StaticManager.particleManager.Stop(particle_selected);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            active_embers = !active_embers;
            if (active_embers == true)
            {
                particle_embers = StaticManager.particleManager.Play(ParticleManager.states.Embers, StaticManager.Character.transform.position);
            }
            else
            {
                StaticManager.particleManager.Stop(particle_embers);
            }
        }
    }
}
