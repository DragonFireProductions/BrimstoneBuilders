using Boo.Lang;

using Kristal;

using UnityEngine;

namespace Assets.Meyer.TestScripts.Player {

    public class Character : Companion {

        [HideInInspector] public static GameObject Player;

        [ SerializeField ] private GameObject camHolder;

    }

}