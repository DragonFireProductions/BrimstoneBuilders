﻿using System.Runtime.CompilerServices;

using Boo.Lang;

using Kristal;

using UnityEngine;

namespace Assets.Meyer.TestScripts.Player {

    public class Character : Companion {

        public static GameObject Player;

        public GameObject cam_pos;
        protected override void Start( ) {
            base.Start();
            Player = gameObject;
        }
        [ SerializeField ] private GameObject camHolder;

    }
   
}