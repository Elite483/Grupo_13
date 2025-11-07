using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public int hearts = 3;
    public int currentHP;
    public int maxHP;

    protected virtual void Start()
    {
        maxHP = hearts * 10;
        currentHP = maxHP;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        Debug.Log($"{gameObject.name} recibió {damage} de daño. HP: {currentHP}/{maxHP}");

        if (IsDead())
            Die();
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        gameObject.SetActive(false);
    }
}

    
