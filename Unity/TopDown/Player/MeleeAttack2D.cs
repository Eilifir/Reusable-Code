using UnityEngine;

public class MeleeAttack2D : MonoBehaviour
{
    //--------------Variables--------------------
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage = 2f;
    [SerializeField] float cooldown = 1f;
    [SerializeField] float lockTime = 0.25f; 
    public bool IsAttacking { get; private set; }
    float cooldownTimer;
    float lockTimer;

    private void Awake()
    {
        if (!attackPoint)
        {
            Transform t = transform.Find("attackPoint");
            if (t) attackPoint = t;
        }
    }

    public void Tick(bool attackPressed, float dt)
    {
        if (cooldownTimer > 0f) cooldownTimer -= dt;

        if (IsAttacking)
        {
            lockTimer -= dt;
            if (lockTimer <= 0f) IsAttacking = false;
        }

        if (attackPressed && cooldownTimer <= 0f && !IsAttacking)
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        if (!attackPoint) return;
        IsAttacking = true;
        lockTimer = lockTime;
        cooldownTimer = cooldown;
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (var col in hits)
        {
            if (col.TryGetComponent(out EnemyHandler enemy))
                enemy.TakeDamage(damage);
        }
    }
}
