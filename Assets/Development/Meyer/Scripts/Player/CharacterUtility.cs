using System;
using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;
using UnityEngine.AI;

public enum DistanceCheck {

    HAS_REACHED_DESTINATION = 0 ,

    HAS_NOT_REACHED_DESTINATION ,

    NAV_MESH_NOT_ENABLED ,

    HAS_NO_PATH ,

    PATH_INVALID,
    
    NOT_ON_NAV

}

namespace Assets.Meyer.TestScripts {

    public class CharacterUtility : MonoBehaviour {

        public DistanceCheck NavDistanceCheck( NavMeshAgent _m_nav_mesh_agent )
        {
            bool activated = false;
            if(!_m_nav_mesh_agent.isOnNavMesh)
                return DistanceCheck.NOT_ON_NAV;
            if ( !_m_nav_mesh_agent.enabled ){
                Debug.Log( "Nav mesh not enabled" );

                return DistanceCheck.NAV_MESH_NOT_ENABLED;
            }

            var l_path = new NavMeshPath( );

            if ( !_m_nav_mesh_agent.pathPending ){
                activated = true;
                if ( _m_nav_mesh_agent.remainingDistance <= _m_nav_mesh_agent.stoppingDistance ){
                    if ( !_m_nav_mesh_agent.hasPath || Math.Abs( _m_nav_mesh_agent.velocity.sqrMagnitude ) < 0.3f ){
                        return DistanceCheck.HAS_REACHED_DESTINATION;
                    }
                }
            }
            else if ( NavMesh.CalculatePath( transform.position , _m_nav_mesh_agent.destination , NavMesh.AllAreas , l_path )
                      && l_path.status == NavMeshPathStatus.PathInvalid ){
                return DistanceCheck.PATH_INVALID;
            }
            else if(!activated)
                return DistanceCheck.HAS_NO_PATH;
            
            return DistanceCheck.HAS_NOT_REACHED_DESTINATION;
        }

    }

}