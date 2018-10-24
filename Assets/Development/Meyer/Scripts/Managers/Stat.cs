using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

using Random = UnityEngine.Random;

public class Stat : MonoBehaviour
{

    struct Info
    {

        private TextMeshProUGUI text;

        private float value;

    }

    [SerializeField] private string name;
    [SerializeField] private float dialogText;
    [SerializeField] private float strength;
    [SerializeField] private float endurance;
    [SerializeField] private float agility;
    [SerializeField] private float charisma;
    [SerializeField] private float perception;
    [SerializeField] private float intelligence;

    [SerializeField] private int attackPoints;

    [SerializeField] private float dexterity;
    [SerializeField] private float luck;

    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] float stamina;
    [SerializeField] float maxStamina;
    private int xp;

    public int maxAttackpoints = 6;

    public int attackCost = 3;

    // Use this for initialization
    void Start()
    {
        health = 100;
        name = this.gameObject.name;
        health = Random.Range( 5 , 7 );

        if ( this.gameObject != StaticManager.character.gameObject ){
        strength = Random.Range( 0 , 30 );
        agility = Random.Range(0, 30);


        }
        endurance = Random.Range( 0 , 30 );
        charisma = Random.Range( 0 , 30 );
        perception = Random.Range( 0 , 30 );
        intelligence = Random.Range( 0 , 30 );
        luck = Random.Range( 0 , 30 );
        attackPoints = maxAttackpoints;
        stamina = Random.Range( 0 , 30 );
        xp = 0;
        maxHealth = 7;
        maxStamina = 30;
        AdjustScale(strength);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Handles the scaling of all characters based on Strength stat
    public void AdjustScale(float _strength)
    {
        if (_strength > 10.0f)
        {
            if (this.gameObject.tag == "Player" || this.gameObject.tag == "Companion" || this.gameObject.tag == "Enemy")
            {
                this.transform.localScale = new Vector3(1, 1, 1);
                float scaling = _strength * 0.03f;
                this.transform.localScale = new Vector3(this.transform.localScale.x + scaling, this.transform.localScale.y + scaling, this.transform.localScale.z + scaling);
                
                
            }
        }
        else 
                this.transform.localScale = new Vector3(1,1,1);
    }

    public void RegenerateAttackPoints()
    {
        this.AttackPoints = Random.Range(0, 4);
    }
    public object this[string propertyName]
    {
        get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
        set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
    }
    public float Charisma
    {
        set { charisma = value; }
        get { return charisma; }
    }
    public float Strength
    {
        set { strength = value; }
        get { return strength; }
    }
    public float Intelligence
    {
        set { intelligence = value; }
        get { return intelligence; }
    }
    public float Endurance
    {
        set { endurance = value; }
        get { return endurance; }
    }
    public float Agility
    {
        set { agility = value; }
        get { return agility; }
    }
    public float Perception
    {
        set { perception = value; }
        get { return perception; }
    }
    public float Luck
    {
        set { luck = value; }
        get { return perception; }
    }

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { MaxHealth = value; }
    }

    public float Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }

    public float MaxStamina
    {
        get { return maxStamina; }
        set { maxStamina = value; }
    }

    public float Dexterity
    {
        get { return dexterity; }
        set { dexterity = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int XP
    {
        get { return xp; }
        set { xp = value; }
    }

    public int AttackPoints
    {
        get { return attackPoints; }
        set { attackPoints = value; }
    }
}
