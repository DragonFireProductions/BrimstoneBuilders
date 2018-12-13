using Kristal;

using TMPro;

using UnityEngine;
using UnityEngine.AI;

public class Stat : MonoBehaviour {

    [ HideInInspector ] public float agility;

    [HideInInspector] public int attackPoints;

    [HideInInspector] public float charisma;

    [HideInInspector] public float dexterity;

    [HideInInspector] public float dialogText;

    [HideInInspector] public float endurance;

    [HideInInspector] public float health;

    [HideInInspector] public float intelligence;

    [HideInInspector] public float luck;

    [HideInInspector] public float maxHealth;

    [HideInInspector] public float maxStamina;

    [HideInInspector] public string name;

    [HideInInspector] public float perception;

    [HideInInspector] public float stamina;

    [HideInInspector] public float strength;

    public object this[string propertyName]
    {
        get
        {
            if (this.GetType().GetField(propertyName) != null)
            {
                return this.GetType().GetField(propertyName).GetValue(this);
            }
            else
            {
                return null;
            }
        }
        set { this.GetType().GetField(propertyName).SetValue(this, value); }
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
    public void Start( ) {
        health = 20;
        name   = gameObject.name;

        if ( gameObject != StaticManager.Character.gameObject ){
           // strength = Random.Range( 0 , 20 );
            agility  = Random.Range( 0 , 30 );
        }

        health = 100;
        endurance    = Random.Range( 0 , 30 );
        charisma     = Random.Range( 0 , 30 );
        perception   = Random.Range( 0 , 30 );
        intelligence = Random.Range( 0 , 30 );
        luck = Random.Range( 1 , 45);
        stamina      = Random.Range( 0 , 30 );
        XP           = 0;
        maxHealth    = 100;
        maxStamina   = 30;
        AdjustScale( strength );
    }

    public Stat baseStats;
    // Update is called once per frame
    public void Update( ) { }

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

    public void ResetStats( ItemStats oldWeapon ) {
        Agility -= oldWeapon.attackSpeed;
        Strength -= oldWeapon.baseDamage + oldWeapon.durability;
        health -= oldWeapon.durability * 5;
    }
    public void IncreaseStats(ItemStats _item) {
       baseStats = new Stat();

        Strength = _item.durability + _item.baseDamage;
        Agility += _item.attackSpeed;
        health += _item.durability * 5;
    }

    public Stat difference(ItemStats _item, ItemStats oldWeapon ) {
        Stat stat = new Stat();

        stat.strength = Strength - (oldWeapon.baseDamage + oldWeapon.durability);
        stat.agility = Agility - oldWeapon.attackSpeed;
        stat.health =  Health - oldWeapon.durability * 5;
        stat.strength =( stat.strength + ( _item.baseDamage + _item.durability ) - Strength);
        stat.health =  ( stat.health + ( _item.durability * 5 )  - Health);
        stat.agility =  ( stat.agility + ( _item.attackSpeed ) - Agility);
        return stat;
    }
    public struct Info {

        public TextMeshProUGUI text;

        public float value;

    }

}