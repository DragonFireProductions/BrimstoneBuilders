using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using TMPro;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    /// <remarks>Set in inspecor</remarks>
    [SerializeField] float WalkSpeed;
    [SerializeField] float RunSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] float Gravity;
    [SerializeField] Transform Cam;

    [SerializeField] float speed = 3;

    CharacterController Controller;
    bool canMove = true;
    float Y;

    float sneakspeed = 1.5f;
    bool sneak = false;

    private float end = 0.0f;

    public static Stat stats;

    //how easily can you be detected when sneaking
    private float dex = 0.0f;
    //how long can you sprint before getting tired
    private float endu = 0.0f;
    //how fast can you walk
    private float agil = 0.0f;

    private bool showstats = false;

    private bool canRun = true;
    private float stamina, maxStamina = 500.0f;

    private float eye_sight = 5.0f;
    private float as_far_as_the_eye_can_see = 15.0f;
    private float blindess = 4.0f;

    [SerializeField]
     EnemyNav[] enemy;

    private float requiredXP = 5.0f;
    private int playerlvl = 1;

    [SerializeField]
    TextMeshProUGUI playerlevel;

    private int isVisible = 1;

    private TextMeshProUGUI sign;

   public enum PlayerState { move, sneak, navMesh}; PlayerState state;

    // Use this for initialization
    void Start() {
        canMove = true;
        state = PlayerState.move;
        if(GetComponent<CharacterController>() != null)
        {
            Controller = GetComponent<CharacterController>();
        }
        else
        {
            gameObject.AddComponent<CharacterController>();
            Controller = GetComponent<CharacterController>();
        }

        Cam = GameObject.Find("CamHolder").transform;
        Assert.IsNotNull(Cam, "Camholder cannot be found!");
        stats = GetComponent<Stat>();
        playerlevel = StaticManager.uiInventory.itemsInstance.Playerlevel.GetComponent < TextMeshProUGUI >( );
        //SetText();
    }

    /// <summary>
    /// Calles the move function onece per frame if character is currently controlled
    /// </summary>
    void Update()
    {
        if (stamina < 10.0f)
            canRun = false;
        else
            canRun = true;



        if (Input.anyKey)
        {
            state = PlayerState.move;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            sneak = !isSneaking();
        }
        if (sneak)
            state = PlayerState.sneak;
        else
            state = PlayerState.move;


        switch (state)
        {
            case PlayerState.move:

                if (!TurnBasedController.instance && canMove)
                    Move();
                break;
            case PlayerState.sneak:
                if (!TurnBasedController.instance && canMove)
                    Sneak();
                break;
            case PlayerState.navMesh:
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            showstats = !Stats();

        }

        if (showstats)
            StaticManager.uiInventory.UpdateStats(stats);
        else if (TurnBasedController.instance && !showstats)
            StaticManager.uiInventory.itemsInstance.StatUI.SetActive(false);


    }

    /// <summary>
    /// Moves character based on current input
    /// </summary>
    void Move()
    {

        float X = Input.GetAxisRaw("Horizontal");
        X *= WalkSpeed;
        float Z = Input.GetAxisRaw("Vertical");
        Z *= WalkSpeed;

        if (X > 0 || Z > 0){
            StaticManager.utility.EnableObstacle( this.gameObject.GetComponent<NavMeshAgent>() , false );
        }

            if (Input.GetKey(KeyCode.LeftShift) && RunSpeed > 1)
            {
                //stamina
                if (canRun)
                {
                    X *= RunSpeed;
                    Z *= RunSpeed;
                    Vector3 n = new Vector3(X, 0, Z);
                    Vector3 dir = Camera.main.transform.TransformDirection(n);
                    dir.y = 0;
                    Controller.Move(dir * RunSpeed * Time.deltaTime);
                    stamina -= 10.0f;
                    endu += 0.005f;
                    //Debug.Log(endu);
                    if (endu > 1.0f)
                    {
                        endu = 0.0f;
                        ++stats.Endurance;
                        maxStamina += 200.0f;
                        ++stats.Health;

                        ++stats.XP;
                        if (stats.XP >= requiredXP)
                        {
                            stats.XP = 0;
                            ++playerlvl;
                            //SetText();
                        }
                    }
                }
            }
        else if (stamina < maxStamina)
                stamina += 10.0f;

        Vector3 norm = new Vector3(X, 0, Z);

        var direction = Camera.main.transform.TransformDirection( norm );
        direction.y = 0;


        Controller.Move(direction.normalized * WalkSpeed * Time.deltaTime);
        if (state == PlayerState.move && X > 0 || X < 0 || Z > 0 || Z < 0)
        {
            agil += 0.005f;
            //Debug.Log("agil:" + agil);
            if (agil > 1.0f)
            {
                agil = 0.0f;
                ++stats.Agility;
                ++WalkSpeed;
                if (WalkSpeed > 7.0f)
                    WalkSpeed = 7.0f;

                ++stats.XP;
                //Debug.Log("XP:" + stats.XP);
                if (stats.XP >= requiredXP)
                {
                    stats.XP = 0;
                    ++playerlvl;
                    //SetText();
                }
            }
        }
    }

    void Sneak()
    {
        float X = Input.GetAxisRaw("Horizontal");
        X *= WalkSpeed;
        float Z = Input.GetAxisRaw("Vertical");
        Z *= WalkSpeed;

        Vector3 n = new Vector3(X, 0, Z);

        var direction = Camera.main.transform.TransformDirection(n);
        direction.y = 0;


        Controller.Move(direction.normalized * sneakspeed * Time.deltaTime);

       // Debug.DrawRay(transform.position, gameObject.transform.forward * eye_sight);
         dex += 0.005f;
         if (dex > 1.0f)
         {
             dex = 0.0f;

             ++stats.Dexterity;
             eye_sight += 0.5f;
             if (eye_sight >= as_far_as_the_eye_can_see)
                 eye_sight = 15.0f;

             ++stats.XP;
             if (stats.XP >= requiredXP)
             {
                 stats.XP = 0;
                 ++playerlvl;
                // SetText();
             }

             for (int i = 0; i < enemy.Length; ++i)
             {
                      //   enemy[i].getVision -= 0.5f;
                       //  if (enemy[i].getVision <= blindess)
//enemy[i].getVision = blindess;
             }
         }
    }

    bool isSneaking()
    {
        return sneak;
    }

    bool Stats()
    {
        return showstats;
    }

    public void SetControlled(bool _control)
    {
        canMove = _control;
    }

    void SetText()
    {
        playerlevel.text = "Player Level:" + playerlvl.ToString();
    }

    public int IsVisible
    {
        get { return isVisible; }
        set { isVisible = value; }
    }

    void OnGUI()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "post")
                {
                    GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 200), "Go forth young warrior, and make your ancestors proud!");

                    //Debug.Log("got the post");
                }
            }
        }
    }
}
