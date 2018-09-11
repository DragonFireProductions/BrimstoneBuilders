using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100; //COME BACK AN MAYBE TURN THIS INTO A STAT SO THAT IT CAN TAKE MODIFIERS?
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    public event System.Action<int, int> OnHealthChanged;


    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        //damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 1, int.MaxValue);

        currentHealth -= damage;

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // DIE IN SOME WAY
        // THIS METHOD IS MEANT TO BE OVERRIDED
    }
}
