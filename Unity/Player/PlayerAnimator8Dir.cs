using UnityEngine;

public class PlayerAnimator8Dir : MonoBehaviour
{
    [SerializeField] Animator anim;
    float lastX = 0f;
    float lastY = -1f;
    private void Awake()
    {
        if (!anim) anim = GetComponent<Animator>();
    }

    public void Apply(Vector2 moveInput, bool running, bool attacking)
    {
        if (!anim) return;
        bool moving = moveInput.sqrMagnitude > 0.001f;
        if (moving && !attacking)
        {
            lastX = moveInput.x;
            lastY = moveInput.y;
        }
        anim.SetBool("IsMoving", moving);
        anim.SetBool("IsRunning", moving && running && !attacking);
        anim.SetBool("IsAttacking", attacking);
        anim.SetFloat("MoveX", lastX);
        anim.SetFloat("MoveY", lastY);
    }
}
