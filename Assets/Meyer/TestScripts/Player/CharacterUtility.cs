using UnityEngine;

namespace Assets.Meyer.TestScripts
{
    public class CharacterUtility : MonoBehaviour
    {
        public static CharacterUtility instance;
        // Use this for initialization
        void Awake () {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        public void rotateTowards(Transform _this, Transform goal, float time)
        {
           
        }
    }
}
