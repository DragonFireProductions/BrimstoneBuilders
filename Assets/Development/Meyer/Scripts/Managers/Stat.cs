using TMPro;

using UnityEngine;

public class Stat : MonoBehaviour {

    [ SerializeField ] private float agility;

    public int attackCost = 3;

    [ SerializeField ] private int attackPoints;

    [ SerializeField ] private float charisma;

    [ SerializeField ] private float dexterity;

    [ SerializeField ] private float dialogText;

    [ SerializeField ] private float endurance;

    [ SerializeField ] private float health;

    [ SerializeField ] private float intelligence;

    [ SerializeField ] private float luck;

    public int maxAttackpoints = 6;

    [ SerializeField ] private float maxHealth;

    [ SerializeField ] private float maxStamina;

    [ SerializeField ] private string name;

    [ SerializeField ] private float perception;

    [ SerializeField ] private float stamina;

    [ SerializeField ] private float strength;

    public object this[ string _property_name ] {
        get { return GetType( ).GetProperty( _property_name ).GetValue( this , null ); }
        set { GetType( ).GetProperty( _property_name ).SetValue( this , value , null ); }
    }

    public float Charisma {
        set { charisma = value; }
        get { return charisma; }
    }

    public float Strength {
        set { strength = value; }
        get { return strength; }
    }

    public float Intelligence {
        set { intelligence = value; }
        get { return intelligence; }
    }

    public float Endurance {
        set { endurance = value; }
        get { return endurance; }
    }

    public float Agility {
        set { agility = value; }
        get { return agility; }
    }

    public float Perception {
        set { perception = value; }
        get { return perception; }
    }

    public float Luck {
        set { luck = value; }
        get { return perception; }
    }

    public float Health {
        get { return health; }
        set { health = value; }
    }

    public float MaxHealth {
        get { return maxHealth; }
        set { MaxHealth = value; }
    }

    public float Stamina {
        get { return stamina; }
        set { stamina = value; }
    }

    public float MaxStamina {
        get { return maxStamina; }
        set { maxStamina = value; }
    }

    public float Dexterity {
        get { return dexterity; }
        set { dexterity = value; }
    }

    public string Name {
        get { return name; }
        set { name = value; }
    }

    public int XP { get; set; }

    public int AttackPoints {
        get { return attackPoints; }
        set { attackPoints = value; }
    }

    // Use this for initialization
    private void Start( ) {
        health = 100;
        name   = gameObject.name;
        health = Random.Range( 5 , 7 );

        if ( gameObject != StaticManager.Character.gameObject ){
            strength = Random.Range( 0 , 20 );
            agility  = Random.Range( 0 , 30 );
        }

        endurance    = Random.Range( 0 , 30 );
        charisma     = Random.Range( 0 , 30 );
        perception   = Random.Range( 0 , 30 );
        intelligence = Random.Range( 0 , 30 );
        luck         = Random.Range( 0 , 30 );
        attackPoints = maxAttackpoints;
        stamina      = Random.Range( 0 , 30 );
        XP           = 0;
        maxHealth    = 7;
        maxStamina   = 30;
        AdjustScale( strength );
    }

    // Update is called once per frame
    private void Update( ) { }

    //Handles the scaling of all characters based on Strength stat
    public void AdjustScale( float _strength ) {
        if ( _strength > 10.0f ){
            if ( gameObject.tag == "Player" || gameObject.tag == "Companion" || gameObject.tag == "Enemy" ){
                transform.localScale = new Vector3( 1 , 1 , 1 );
                var l_scaling = _strength * 0.03f;
                transform.localScale = new Vector3( transform.localScale.x + l_scaling , transform.localScale.y + l_scaling , transform.localScale.z + l_scaling );
            }
        }
        else{
            transform.localScale = new Vector3( 1 , 1 , 1 );
        }
    }

    public void RegenerateAttackPoints( ) {
        AttackPoints = Random.Range( 0 , 4 );
    }

    private struct Info {

        private TextMeshProUGUI text;

        private float value;

    }

}