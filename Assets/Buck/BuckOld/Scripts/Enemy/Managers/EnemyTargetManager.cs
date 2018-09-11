using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetManager : MonoBehaviour
{
    public List<Transform> targets;

    //Accessor
    public List<Transform> GetTargets()
    {
        return targets;
    }

    public void AddAllTargets()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //GameObject fire = GameObject.FindGameObjectWithTag("Fire");
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        //GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");

        //AddTarget(player.transform);
        //AddTarget(fire.transform);

        foreach (GameObject turret in turrets)
        {
            AddTarget(turret.transform);

            if (turrets == null)
                return;
        }

        //foreach (GameObject wall in walls)
        //{
        //    AddTarget(wall.transform);

        //    if (walls == null)
        //        return;
        //}
    }

    public void AddTarget(Transform target)
    {
        targets.Add(target);
    }

    public void RemoveTarget(Transform target)
    {
        //Change this to an int that tracks place in list
        if (targets.Count > 0)
        {
            targets.Remove(target);
        }
    }
}
