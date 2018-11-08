using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;

public class CompanionNav : BaseNav {

    private float randDistance;

    private int des;

    private int currTarget;

    public void Start( ) {
        base.Start( );
        SetState          = state.IDLE;
        character         = GetComponent < Companion >( );
        randDistance = Random.Range( 1.5f, 1.5f + 2);
        battleDistance = 4;
        des = LineManager.assignIndex( );
    }

    protected override void Update( ) {
        switch ( State ){
            case state.IDLE:
            {
                Agent.destination = Character.Player.transform.position + (Vector3.right  * des);
            }

                break;
            case state.CHILL:
            {
                Agent.destination = Character.Player.transform.position + (Vector3.right * des);
            }
                break;
            case state.BERZERK:
            {
                if (StaticManager.RealTime.Enemies.Count > 0)
                {
                    for (int i = 0; i < StaticManager.RealTime.Enemies.Count; ++i)
                    {
                        float distance = Vector3.Distance(transform.position,
                            StaticManager.RealTime.Enemies[i].transform.position);

                        if (distance < 10.0f)
                        {
                            currTarget = i;
                            break;
                        }
                    }

                    Agent.SetDestination(StaticManager.RealTime.Enemies[currTarget].transform.position);
                }
            }
                break;
            default:

                break;
        }
    }

}