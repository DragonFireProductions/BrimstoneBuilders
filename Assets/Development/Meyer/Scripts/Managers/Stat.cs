using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.AI;

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

    [ SerializeField ] private float maxHealth;

    [ SerializeField ] private float maxStamina;

    [ SerializeField ] private string name;

    [ SerializeField ] private float perception;

    [ SerializeField ] private float stamina;

    [ SerializeField ] private float strength;

    public int maxAttackpoints = 6;


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

    public void UpdateStrength( ) {
        gameObject.GetComponent<Enemy>().threat_signal.enabled = Strength > 10;
    }
    // Use this for initialization
    private void Start( ) {
        health = 20;
        name   = gameObject.name;

        if ( gameObject != StaticManager.Character.gameObject ){
           // strength = Random.Range( 0 , 20 );
            agility  = Random.Range( 0 , 30 );
        }

        if ( gameObject == StaticManager.Character.gameObject ){
            health = 100;
        }
        else{
        health = Random.Range( 5 , 100 );

        }
        endurance    = Random.Range( 0 , 30 );
        charisma     = Random.Range( 0 , 30 );
        perception   = Random.Range( 0 , 30 );
        intelligence = Random.Range( 0 , 30 );
        luck         = Random.Range( 0 , 30 );
        attackPoints = maxAttackpoints;
        stamina      = Random.Range( 0 , 30 );
        XP           = 0;
        maxHealth    = 100;
        maxStamina   = 30;
        AdjustScale( strength );
    }

    private Stat baseStats;
    // Update is called once per frame
    private void Update( ) { }

    //Handles the scaling of all characters based on Strength stat
    public void AdjustScale( float _strength ) {
        //if ( _strength > 10.0f ){
        //    if ( gameObject.tag == "Player" || gameObject.tag == "Companion" || gameObject.tag == "Enemy" ){
        //        transform.localScale = new Vector3( 1 , 1 , 1 );
        //        var l_scaling = _strength * 0.03f;
        //        transform.localScale = new Vector3( transform.localScale.x + l_scaling , transform.localScale.y + l_scaling , transform.localScale.z + l_scaling );
        //        gameObject.GetComponent < NavMeshAgent >( ).radius += 0.5f;
        //        gameObject.GetComponent < BaseNav >( ).stoppingDistance = 5;
        //    }
        //}
        //else{
        //    transform.localScale = new Vector3( 1 , 1 , 1 );
        //}
    }

    public void ResetStats( WeaponItem oldWeapon ) {
        Agility -= oldWeapon.attackSpeed;
        Strength -= oldWeapon.baseDamage + oldWeapon.durability;
        health -= oldWeapon.durability * 5;
    }
    public void IncreaseStats(WeaponItem _item) {
       baseStats = new Stat();

        Strength = _item.durability + _item.baseDamage;
        Agility += _item.attackSpeed;
        health += _item.durability * 5;
    }

    public Stat difference(WeaponItem _item, WeaponItem oldWeapon ) {
        Stat stat = new Stat();

        stat.strength = Strength - (oldWeapon.baseDamage + oldWeapon.durability);
        stat.agility = Agility - oldWeapon.attackSpeed;
        stat.health =  Health - oldWeapon.durability * 5;
        stat.strength =( stat.strength + ( _item.baseDamage + _item.durability ) - Strength);
        stat.health =  ( stat.health + ( _item.durability * 5 )  - Health);
        stat.agility =  ( stat.agility + ( _item.attackSpeed ) - Agility);
        return stat;
    }
    private struct Info {

        private TextMeshProUGUI text;

        private float value;

    }

}