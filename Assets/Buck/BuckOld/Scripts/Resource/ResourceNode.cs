using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceNode : MonoBehaviour
{
    bool inRange;
    bool isSpawned = true;

    public int maxHealth;
    int curHealth;

    public int minResource, maxResource;

    public float respawnTime;
    float respawnTimer;

    public GameObject resourcePrefab;

    AudioSource resourceAudio;
    public AudioClip[] resourceSounds;
    public AudioClip[] resourceDestroyAudio;

    MeshRenderer[] childMR;
    BoxCollider[] childCollider;
    Canvas pfiCanvas;

	void Start ()
    {
        resourceAudio = GetComponent<AudioSource>();
        resourceAudio.enabled = false;
        childMR = gameObject.GetComponentsInChildren<MeshRenderer>();
        childCollider = gameObject.GetComponentsInChildren<BoxCollider>();
        pfiCanvas = GetComponentInChildren<Canvas>();
        pfiCanvas.enabled = false;
        curHealth = maxHealth;
    }
	
	void Update ()
    {
        DamageResource();
        RespawnCheck();
        LookAtCamera();
	}

    void LookAtCamera()
    {
        if (pfiCanvas.enabled)
        {
            pfiCanvas.transform.rotation = Camera.main.transform.rotation;
        }
        else
            return;
    }

    void RespawnCheck()
    {
        if (curHealth <= 0 && !isSpawned)
        {
            respawnTimer += Time.deltaTime;

            if (respawnTimer >= respawnTime && !isSpawned)
            {
                for (int r = 0; r < childMR.Length; r++)
                    for (int c = 0; c < childCollider.Length; c++)
                    {
                        childMR[r].enabled = true;
                        childCollider[c].enabled = true;
                    }
                curHealth = maxHealth;
                respawnTimer = 0;
                resourceAudio.enabled = true;
                isSpawned = true;
            }
            else
                return;
        }
    }

    void DamageResource()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E))
        {
            curHealth--;

            int chosenAudioClip = Random.Range(0, resourceSounds.Length);

            resourceAudio.clip = resourceSounds[chosenAudioClip];

            resourceAudio.Play();

            curHealth = Mathf.Clamp(0, curHealth, maxHealth);

            if (curHealth <= 0 && isSpawned)
            {

                ResourceDestroyed();
            }
        }
        else
            return;
    }

    void ResourceDestroyed()
    {
        int resourceReward = Random.Range(minResource, maxResource);

        int chosenDestroyClip = Random.Range(0, resourceDestroyAudio.Length);

        resourceAudio.clip = resourceDestroyAudio[chosenDestroyClip];

        resourceAudio.Play();

        for (int r = 0; r < childMR.Length; r++)
            for (int c = 0; c < childCollider.Length; c++)
            {
                childMR[r].enabled = false;
                childCollider[c].enabled = false;
            }

        for (int r = 0; r < resourceReward; r++)
        {
            Instantiate(resourcePrefab, transform.position, transform.rotation);
        }

        Invoke("DisableAudioSource", resourceAudio.clip.length);
        pfiCanvas.enabled = false;
        isSpawned = false;
    }

    void DisableAudioSource()
    {
        resourceAudio.enabled = false;
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
            inRange = true;
        resourceAudio.enabled = true;
        pfiCanvas.enabled = true;

    }

    void OnTriggerExit(Collider player)
    {
        if (player.tag == "Player")
            inRange = false;
        resourceAudio.enabled = false;
        pfiCanvas.enabled = false;
    }
}
