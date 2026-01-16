using UnityEngine;

public class InputReader : MonoBehaviour
{
    public Vector3 direction;
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponenet<Rigidbody>();
    }
    public void ReadInput()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");
        direction = new Vector3(xAxis, rb.linearVelocity.y, zAxis);
    }
}
