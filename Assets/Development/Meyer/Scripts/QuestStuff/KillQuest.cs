using System.Collections;
using System.Collections.Generic;

using Kristal;

using UnityEngine;

public class KillQuest : Quest {

   [HideInInspector] public int EnemiesKilled = 0;

    public int KillAmount;

    public List < Enemy > enemies;

    public GameObject winObject;

    public GameObject spawner;

    private bool accepted = false;
    protected override void Accept( ) {
        base.Accept();
        spawner.SetActive(true);
        accepted = true;
    }

    public override void InstEnemies(Enemy enemy ) {
        enemies.Add(enemy);
    }

    public override void EnemyDied( Enemy enemy ) {
        enemies.Remove( enemy );
    }

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (accepted &&  enemies.Count <= 0 ){
            ReturnToNPC();
            Completed = true;
        }
       
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (Completed)
            {
                var key = Instantiate(winObject);
                key.transform.position = transform.position;
                key.gameObject.SetActive(true);
                gameObject.GetComponent<Collider>().enabled = false;
            }
            else if ( enemies.Count == 0 && !Completed ){
                Accept();
            }
            else
            {
                StaticManager.uiManager.ShowMessage("Please open quest log to see your current objective", 5);
            }

        }
    }
}
