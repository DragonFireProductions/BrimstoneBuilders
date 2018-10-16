using System;
using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;
using UnityEngine.AI;
public enum DistanceCheck
{

    HasReachedDestination = 0,

    HasNotReachedDestination,

    NavMeshNotEnabled,

    HasNoPath,

    PathInvalid

}

namespace Assets.Meyer.TestScripts
{
    public class CharacterUtility : MonoBehaviour
    {
    
        
        
        public void EnableObstacle( NavMeshAgent game_object, bool agentMoving = false) {
            game_object.enabled = agentMoving;
            game_object.gameObject.GetComponent < NavMeshObstacle >( ).enabled = !agentMoving;
        }
        public void quickSort(float[] A, int left, int right, int[] indexs)
        {
            if (left > right || left < 0 || right < 0) return;

            int index = partition(A, left, right, indexs);

            if (index != -1)
            {
                quickSort(A, left, index - 1,indexs);
                quickSort(A, index + 1, right, indexs);
            }
        }
        

        public DistanceCheck NavDistanceCheck( NavMeshAgent mNavMeshAgent ) {

            if ( !mNavMeshAgent.enabled){
                Debug.Log("Nav mesh not enabled");

                return DistanceCheck.NavMeshNotEnabled;
            }
            NavMeshPath path = new NavMeshPath();
            ;
                if (!mNavMeshAgent.pathPending)
            {
                if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
                {
                    if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f){
                        return DistanceCheck.HasReachedDestination;
                    }
                }
            }
                else if ( NavMesh.CalculatePath( transform.position , mNavMeshAgent.destination , NavMesh.AllAreas , path )
                      && path.status == NavMeshPathStatus.PathInvalid  ){
                    return DistanceCheck.PathInvalid;
                }

            return DistanceCheck.HasNotReachedDestination;
        }

        public bool NavDistanceCheck(List <Enemy> mNavMeshAgent)
        {
            for ( int i = 0 ; i < mNavMeshAgent.Count ; i++ ){
                if ( !mNavMeshAgent[i].Nav.Agent.pathPending ){
                    if ( mNavMeshAgent[i].Nav.Agent.remainingDistance <= mNavMeshAgent[i].Nav.Agent.stoppingDistance ){
                        if ( !mNavMeshAgent[i].Nav.Agent.hasPath || mNavMeshAgent[i].Nav.Agent.velocity.sqrMagnitude == 0f ){
                            return true;
                        }
                    }
                }
            }

            return false;
        }

       


        public void SetDestination ( NavMeshAgent character, Vector3 Destination ) {
            StartCoroutine( checkDestination( character , Destination ) );
        }

        IEnumerator checkDestination ( NavMeshAgent character, Vector3 destination) {
            if ( StaticManager.utility.NavDistanceCheck( character ) == DistanceCheck.HasNotReachedDestination){
                yield return new WaitForEndOfFrame( );
            }
            else{
                character.SetDestination( destination );
            }
        }

        public bool CheckRotation( Quaternion rotation , Quaternion rotation2 ) {
            if ( rotation != rotation2  ){
                return false;
            }

            return true;
        }
        public bool CheckRotation(List<Enemy> obj, Quaternion rotation2)
        {

            for ( int i = 0 ; i < obj.Count ; i++ ){
                if (obj[i].gameObject.transform.rotation != rotation2)
                {
                    return false;
                }
            }
            return true;
        }
        public List <Enemy> CheckRotationList(List<Enemy> obj, Quaternion rotation2)
        {
            List <Enemy> list = new List < Enemy >();
            for (int i = 0; i < obj.Count; i++)
            {
                if (obj[i].gameObject.transform.rotation != rotation2)
                {
                    list.Add(obj[i]);
                }
            }

            return list;
        }
        public List<Companion> CheckRotationList(List<Companion> obj, Quaternion rotation2)
        {
            List<Companion> list = new List<Companion>();
            for (int i = 0; i < obj.Count; i++)
            {
                if (obj[i].gameObject.transform.rotation != rotation2)
                {
                    list.Add(obj[i]);
                }
            }

            return list;
        }
        public bool CheckRotation(List<Companion> obj, Quaternion rotation2)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                if (obj[i].gameObject.transform.rotation != rotation2)
                {
                    return false;
                }
            }
            return true;
        }
        private static int partition(float[] A, int left, int right, int[] index)
        {
            if (left > right) return -1;

            int end = left;

            float pivot = A[right];    // choose last one to pivot, easy to code
            for (int i = left; i < right; i++)
            {
                if (A[i] < pivot)
                {
                    swap(A, i, end,index);
                    end++;
                }
            }

            swap(A, end, right,index);

            return end;
        }

        private static void swap(float[] A, int left, int right, int[] indexs)
        {
            float tmp = A[left];
            int tempObj = indexs[left ];

            A[left] = A[right];
            indexs[ left ] = indexs[ right ];

            A[right] = tmp;
            indexs[ right ] = tempObj;
        }

    }


}
