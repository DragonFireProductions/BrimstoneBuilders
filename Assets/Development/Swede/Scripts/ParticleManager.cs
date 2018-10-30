using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [HideInInspector] private ParticleSystem[] particleNames;
    [SerializeField] private ParticleSystem[] Systems;

    public GameObject ParticleHolder;
    public enum states { Rain = 0, Smoke, Selected, Embers }

    public static string GetName(int index)
    {
        states StateName = (states) index;
        return StateName.ToString();
    }
    public void Start()
    {
        ParticleHolder = GameObject.Find("Particles");

        int lastIndex = states.GetValues(typeof(states)).Cast<int>().Last();
        particleNames = new ParticleSystem[lastIndex + 1];
        Systems = new ParticleSystem[lastIndex + 1];

        for (int i = 0; i <= lastIndex; i++)
        {
            particleNames[i] = GameObject.Find(GetName(i)).GetComponent<ParticleSystem>();
        }
        for (int i = 0; i < particleNames.Length; i++)
        {
            for (int j = 0; j < particleNames.Length; j++)
            {
                if (particleNames[i].name == GetName(j))
                {
                    Systems[j] = particleNames[i];
                    Systems[j].Stop();
                    Systems[j].gameObject.SetActive(false);
                }
            }
        }
    }
    public ParticleSystem Play(states state, Vector3 position)
    {
        GameObject newsystem = Instantiate(Systems[(int) state].gameObject, position, Systems[(int) state].gameObject.transform.rotation);

        newsystem.SetActive(true);
        newsystem.GetComponent<ParticleSystem>().Play();
        newsystem.transform.SetParent(ParticleHolder.transform);
        return newsystem.GetComponent<ParticleSystem>();
    }
    public ParticleSystem Play(states state, Vector3 position, Transform parent)
    {
        GameObject newsystem = Instantiate(Systems[(int)state].gameObject, position, Systems[(int) state].gameObject.transform.rotation);

        newsystem.SetActive(true);
        newsystem.GetComponent<ParticleSystem>().Play();
        newsystem.transform.SetParent(parent);
        return newsystem.GetComponent<ParticleSystem>();
    }
    public void Stop(ParticleSystem system)
    {
        system.Stop();
        Destroy(system.gameObject);
    }
}
