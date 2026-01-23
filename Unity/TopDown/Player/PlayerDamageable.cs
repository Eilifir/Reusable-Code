using UnityEngine;

public class PlayerDamageable : Damageable
{
    [SerializeField] Animator anim;
    [SerializeField] MonoBehaviour[] disableOnDeath;
    protected override void Awake()
    {
        base.Awake();
        if (!anim) anim = GetComponent<Animator>();
    }
    protected override void OnDamaged(float amount)
    {
        if (anim) anim.SetTrigger("Hit"); 
    }
    protected override void OnDeath()
    {
        if (anim) anim.SetTrigger("Death");

        for (int i = 0; i < disableOnDeath.Length; i++)
            if (disableOnDeath[i]) disableOnDeath[i].enabled = false;
    }
}
