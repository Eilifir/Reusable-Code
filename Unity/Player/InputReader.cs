using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    //------------------Variables-----------------
    [SerializeField] KeyCode runKey = KeyCode.LeftShift;
    [SerializeField] KeyCode attackKey = KeyCode.F;
    public Vector2 Move { get; private set; }
    public bool RunHeld { get; private set; }
    public bool AttackPressed { get; private set; }

    public void ReadInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move = new Vector2(h, v).normalized;
        RunHeld = Input.GetKey(runKey);
        AttackPressed = Input.GetKeyDown(attackKey);
    }
}
