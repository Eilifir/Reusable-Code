using UnityEngine;

public class CharacterMotor2D : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    private Vector2 desiredVelocity;
    private bool locked;

    private void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
    }

    public void SetLocked(bool value)
    {
        locked = value;
        if (locked) desiredVelocity = Vector2.zero;
    }

    public void SetDesiredVelocity(Vector2 velocity)
    {
        desiredVelocity = velocity;
    }

    private void FixedUpdate()
    {
        rb.velocity = locked ? Vector2.zero : desiredVelocity;
    }
}
