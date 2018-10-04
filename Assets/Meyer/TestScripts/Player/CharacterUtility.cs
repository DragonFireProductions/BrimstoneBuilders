using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

namespace Assets.Meyer.TestScripts
{
    public class CharacterUtility : MonoBehaviour
    {
        public static CharacterUtility instance;

        public TurnBased turnbased;
        public static bool isRotated;
        // Use this for initialization
        void Awake () {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            
            turnbased.enabled = false;
        }
	
        // Update is called once per frame
        void Update () {
		
        }

       public IEnumerator rotateTowards(GameObject game_object, Quaternion rotation)
        {
            while (game_object.transform.localRotation != rotation){
                isRotated = false;
                game_object.transform.rotation = Quaternion.Slerp(game_object.transform.rotation, rotation, 5 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            isRotated = true;
        }

        public IEnumerator rotateTowardsSelect(GameObject game_object, Quaternion rotation)
        {
            while (game_object.transform.rotation != rotation)
            {
                isRotated = false;
                game_object.transform.rotation = Quaternion.Lerp(game_object.transform.rotation, rotation, 5 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            TurnBased.Instance.SelectPlayer();
        }
        public void EnableObstacle( GameObject game_object, bool agentMoving = false) {
            game_object.GetComponent<NavMeshAgent>().enabled = agentMoving;
            game_object.GetComponent<NavMeshObstacle>().enabled = !agentMoving;
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
        

        public bool NavDistanceCheck( GameObject obj ) {
            var mNavMeshAgent = obj.GetComponent < NavMeshAgent >( );
            if (!mNavMeshAgent.pathPending)
            {
                if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
                {
                    if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f){
                        return true;
                    }
                }
            }

            return false;
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
