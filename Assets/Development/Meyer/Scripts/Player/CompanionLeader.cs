//using System.Collections;
//using System.Collections.Generic;

//using Assets.Meyer.TestScripts;

//using UnityEngine;
//using UnityEngine.AI;

//public class CompanionLeader : Companion {

//	//[SerializeField] List < GameObject > companions;

//	//public List < Companion > CompanionGroup;

//    // Use this for initialization

//    void Awake () {
//		base.Awake();
//	    this.material.color = LeaderColor;
//	    camHolder = gameObject.transform.Find("CamHolder").gameObject;
//	    leader = this.gameObject.GetComponent<CompanionLeader>();

//		foreach ( var VARIABLE in characterObjs){
//			characters.Add(VARIABLE.GetComponent<Companion>());
//		}
//	}
//    public void Remove(BaseCharacter _obj)
//    {
//        characters.Remove(_obj);
//        characterObjs.Remove(_obj.gameObject);
//        //TurnBasedController.instance._player.characters.Remove((Companion)_obj);
//        Destroy(_obj.obj);
//    }



//}
