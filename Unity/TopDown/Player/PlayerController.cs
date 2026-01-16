using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //----------------References--------------------
    [SerializeField] PlayerInputReader inputReader;
    [SerializeField] CharacterMotor2D motor;
    [SerializeField] Stamina stamina;
    [SerializeField] MeleeAttack2D attack;
    [SerializeField] PlayerAnimator8Dir animator8Dir;
    //----------------Variables--------------------
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;

    private void Update()
    {
        inputReader.ReadInput();
        attack.Tick(inputReader.AttackPressed, Time.deltaTime);
        bool wantsRun = inputReader.RunHeld && inputReader.Move.sqrMagnitude > 0.001f;
        bool canRun = stamina && stamina.HasStamina;
        bool isRunning = wantsRun && canRun && !attack.IsAttacking;
        if (stamina)
            stamina.Tick(isRunning, Time.deltaTime);
        if (motor)
        {
            motor.SetLocked(attack.IsAttacking);
            float speed = isRunning ? runSpeed : walkSpeed;
            motor.SetDesiredVelocity(inputReader.Move * speed);
        }
        if (animator8Dir)
            animator8Dir.Apply(inputReader.Move, isRunning, attack.IsAttacking);
    }
}
