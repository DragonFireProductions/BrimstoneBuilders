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
   // /// <remarks>Set in inspecor</remarks>
   // [SerializeField] float WalkSpeed;
   // [SerializeField] float RunSpeed;
   // [SerializeField] float JumpForce;
   // [SerializeField] float Gravity;
   // [SerializeField] Transform Cam;

   // [SerializeField] float speed = 3;

   // CharacterController Controller;
   // bool canMove = true;
   // float Y;

   // float sneakspeed = 1.5f;
   // bool sneak = false;

   // private float end = 0.0f;

   // public static Stat stats;

   // //how easily can you be detected when sneaking
   // private float dex = 0.0f;
   // //how long can you sprint before getting tired
   // private float endu = 0.0f;
   // //how fast can you walk
   // private float agil = 0.0f;

   // private bool showstats = false;

   // private bool canRun = true;
   // private float stamina, maxStamina = 500.0f;

   // private float eye_sight = 5.0f;
   // private float as_far_as_the_eye_can_see = 15.0f;
   // private float blindess = 4.0f;

   // [SerializeField]
   //  EnemyNav[] enemy;

   // private float requiredXP = 5.0f;
   // private int playerlvl = 1;

   // [SerializeField]
   // Text playerlevel;

   // private int isVisible = 1;

   //public enum PlayerState { move, sneak, navMesh}; PlayerState state;

   // // Use this for initialization
   // void Start() {
   //     canMove = true;
   //     state = PlayerState.move;
   //     if(GetComponent<CharacterController>() != null)
   //     {
   //         Controller = GetComponent<CharacterController>();
   //     }
   //     else
   //     {
   //         gameObject.AddComponent<CharacterController>();
   //         Controller = GetComponent<CharacterController>();
   //     }

   //     Cam = GameObject.Find("CamHolder").transform;
   //     Assert.IsNotNull(Cam, "Camholder cannot be found!");
   //     stats = GetComponent<Stat>();
   //     playerlevel = StaticManager.uiInventory.itemsInstance.Playerlevel.GetComponent < Text >( );
   //     SetText();
   // }

   // /// <summary>
   // /// Calles the move function onece per frame if character is currently controlled
   // /// </summary>
   // void Update()
   // {
   //     if (stamina < 10.0f)
   //         canRun = false;
   //     else
   //         canRun = true;
   //     //Debug.DrawRay(transform.position, gameObject.transform.forward * eye_sight);

   //     if (Input.anyKey)
   //     {
   //         state = PlayerState.move;
   //     }

   //     if (Input.GetKeyDown(KeyCode.C))
   //     {
   //         sneak = !isSneaking();
   //     }
   //     if (sneak)
   //         state = PlayerState.sneak;
   //     else
   //         state = PlayerState.move;

        
        


   // }
}
