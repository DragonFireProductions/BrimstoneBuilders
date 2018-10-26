using UnityEngine;

public class DamageCalc : MonoBehaviour {

    // Use this for initialization
    private void Awake( ) { }

    // Update is called once per frame
    private void Update( ) { }

    public float CalcAttack( Stat victim , Stat attacker ) {
        float l_damageMultipler = 5;

        if ( attacker.Strength > victim.Strength ){
            l_damageMultipler *= 2;
            victim.Strength -= 5;
        }

        if ( attacker.Agility < victim.Agility ){
            l_damageMultipler *= 0.8f;
        }

        if ( attacker.Endurance < victim.Endurance ){
            l_damageMultipler *= 0.8f;
        }

        return l_damageMultipler;
    }

}