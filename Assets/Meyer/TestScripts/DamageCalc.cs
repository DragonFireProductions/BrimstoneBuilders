using System.Collections.Generic;

using UnityEngine;

public class DamageCalc : MonoBehaviour {

    public static DamageCalc Instance;

    // Use this for initialization
    private void Awake( ) {
        if ( Instance == null ){
            Instance = this;
        }
        else if ( Instance != this ){
            Destroy( gameObject );
        }

        DontDestroyOnLoad( gameObject );
    }

    // Update is called once per frame
    private void Update( ) { }

    public float CalcAttack( GameObject _victim , GameObject _attacker ) {
        var attacker = _attacker.GetComponent < Stat >( );
        var victim   = _victim.GetComponent < Stat >( );

        float damageMultipler = 5;

        if ( attacker.Strength > victim.Strength ){
            damageMultipler *= 2;
            victim.Strength -= 5;
        }

        if ( attacker.Agility < victim.Agility ){
            damageMultipler *= 0.8f;
        }

        if ( attacker.Endurance < victim.Endurance ){
            damageMultipler *= 0.8f;
        }

        return damageMultipler;
    }
   
}