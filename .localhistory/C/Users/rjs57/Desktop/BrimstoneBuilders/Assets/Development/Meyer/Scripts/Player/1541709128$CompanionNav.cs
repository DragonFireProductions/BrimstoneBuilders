using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using Kristal;

using UnityEngine;

public class CompanionNav : BaseNav {

    private float randDistance;

    private int des;

    public void Start( ) {
        base.Start( );
        SetState          = state.IDLE;
        character         = GetComponent < Companion >( );
        randDistance = Random.Range( 1.5f, 1.5f + 2);
        battleDistance = 4;
        des = LineManager.assignIndex( );
    }

    protected override void Update( ) {
        base.Update();
        switch ( State ){
            case state.IDLE: {
                Agent.destination = Character.Player.transform.position + (Vector3.right  * des);
                        }

                break;
            default:

                break;
        }
    }

}