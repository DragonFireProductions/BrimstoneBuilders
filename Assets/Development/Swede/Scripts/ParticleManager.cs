using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [HideInInspector] private ParticleSystem[] particleNames;
    [SerializeField] private ParticleSystem[] Systems;


    //local variable for particle system, gets set in Start.
    public GameObject ParticleHolder;


    //There's a state enum for every particle system type.
    //If more particle systems are added, a state also needs to be added with the exact same name as the particle system.
    //Order does not need to match hierarchy.
    public enum states
    {
        Rain = 0,
        Smoke,
        Selected,
        Embers,
        Damage,
        Spawn,
        Shock,
        Click,
        Snow,
        Leaves,
        EnemySelected
    }


    //Returns the State(particle system) at the given index.
    public static string GetName(int index)
    {
        var StateName = (states) index;
        return StateName.ToString();
    }

    public void Awake()
    {
        //Local variable grabs the whole particle system GameObject from the hierarchy
        //GameObject -MUST- be called "Particles"
        ParticleHolder = GameObject.Find("Particles");


        //Finds the amount of particle systems, sets the lists to that size.
        var lastIndex = Enum.GetValues(typeof(states)).Cast<int>().Last();
        particleNames = new ParticleSystem[lastIndex + 1];
        Systems = new ParticleSystem[lastIndex + 1];


        //fills the particleNames list with the names of all the particle systems.
        for(var i = 0; i <= lastIndex; i++)
            particleNames[i] = GameObject.Find(GetName(i)).GetComponent<ParticleSystem>();


        //loops through both lists, makes sure the particle systems have the same index.
        //Also stops any active particle systems.
        for(var i = 0; i < particleNames.Length; i++)
        {
            for(var j = 0; j < particleNames.Length; j++)
                if(particleNames[i].name == GetName(j))
                {
                    Systems[j] = particleNames[i];
                    Systems[j].Stop();
                    Systems[j].gameObject.SetActive(false);
                }
        }
    }


    //Instantiates the selected particle state at the selected position.
    public ParticleSystem Play(states state, Vector3 position)
    {
        var newsystem = Instantiate(Systems[(int) state].gameObject);
        newsystem.transform.position = position;

        newsystem.SetActive(true);
        newsystem.GetComponent<ParticleSystem>().Play();
        newsystem.transform.SetParent(ParticleHolder.transform);
        return newsystem.GetComponent<ParticleSystem>();
    }

    //Instantiates the selected particle state at the selected position for the passed in amount of time.
    public IEnumerator Play(ParticleManager.states state, Vector3 position, int time)
    {
        ParticleSystem system = StaticManager.particleManager.Play(state, position);
        yield return new WaitForSeconds(time);
        StaticManager.particleManager.Stop(system);
    }
    //Instantiates the selected particle state at the selected position and parents the particle system to the selected parent.
    public ParticleSystem Play(states state, Vector3 position, Transform parent)
    {
        var newsystem = Instantiate(Systems[(int) state].gameObject, position,
            Systems[(int) state].gameObject.transform.rotation);

        newsystem.SetActive(true);
        newsystem.GetComponent<ParticleSystem>().Play();
        newsystem.transform.SetParent(parent);
        return newsystem.GetComponent<ParticleSystem>();
    }


    //Destroys the selected particle system.
    public void Stop(ParticleSystem system)
    {
        system.Stop();
        Destroy(system.gameObject);
    }
}
