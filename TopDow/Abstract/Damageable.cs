using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    //-------------Variables---------------
    [SerializeField] protected float maxHealth = 10f;
    public float CurrentHealth { get; private set; }
    public float MaxHealth => maxHealth;
    public bool IsDead => CurrentHealth <= 0f;

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        if (IsDead) return;
        if (amount <= 0f) return;
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);
        OnDamaged(amount);
        if (IsDead)
            OnDeath();
    }

    public virtual void Heal(float amount)
    {
        if (IsDead) return;
        if (amount <= 0f) return;

        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);
    }
    protected virtual void OnDamaged(float amount) { }
    protected abstract void OnDeath();
}
