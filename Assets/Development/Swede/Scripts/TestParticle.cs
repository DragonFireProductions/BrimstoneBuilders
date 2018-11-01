using UnityEngine;


//Test script to ensure particles fire off properly.
public class TestParticle : MonoBehaviour
{
    //All particle systems being tested must have a bool and matching Particle system.
    private bool active_rain, active_smoke, active_selected, active_embers;
    private ParticleSystem particle_rain, particle_smoke, particle_selected, particle_embers;

    private void Start()
    {
    }

    private void Update()
    {
        //If corresponding number is pressed, flips the bool. Then, if true, calls play from the Particle Manager script.

        if(Input.GetKeyDown(KeyCode.Alpha1)) //Number 1
        {
            //Flips bool
            active_rain = !active_rain;

            //If bool is true, calls play from the ParticleManager to play the ParticleSystem at the player's position.
            if(active_rain)
                particle_rain = StaticManager.particleManager.Play(ParticleManager.states.Rain,
                    StaticManager.Character.transform.position);
            else
                StaticManager.particleManager.Stop(particle_rain);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) //Number 2
        {
            active_smoke = !active_smoke;
            if(active_smoke)
                particle_smoke = StaticManager.particleManager.Play(ParticleManager.states.Smoke,
                    StaticManager.Character.transform.position);
            else
                StaticManager.particleManager.Stop(particle_smoke);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)) //Number 3
        {
            active_selected = !active_selected;
            if(active_selected)
                particle_selected = StaticManager.particleManager.Play(ParticleManager.states.Selected,
                    StaticManager.Character.transform.position, StaticManager.Character.transform);
            else
                StaticManager.particleManager.Stop(particle_selected);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)) //Number 4
        {
            active_embers = !active_embers;
            if(active_embers)
                particle_embers = StaticManager.particleManager.Play(ParticleManager.states.Embers,
                    StaticManager.Character.transform.position);
            else
                StaticManager.particleManager.Stop(particle_embers);
        }
    }
}