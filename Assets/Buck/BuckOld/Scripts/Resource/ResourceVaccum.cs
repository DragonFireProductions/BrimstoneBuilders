using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceVaccum : MonoBehaviour
{
    Transform player;

    [SerializeField]
    float distanceFromPlayer;

    [SerializeField]
    float vaccumSpeed;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update ()
    {
        MoveToPlayer();
	}

    void MoveToPlayer()
    {
        if (Vector3.Distance(player.position, transform.position) <= distanceFromPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, vaccumSpeed * Time.deltaTime);
        }
    }
}
