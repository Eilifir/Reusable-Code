using UnityEngine;
using UnityEngine.SceneManagement;
//Quick disclaimer all of the values are selected at random, feel free to tune them to your heart's content
//Also your player need to have the components listed in References + an empty GameObject called "attackPoint"
public class PlatformerMovement : MonoBehaviour
{
    //-------------------References-------------------
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    [SerializeField]Transform attackPoint;        
    [SerializeField]Transform groundCheck;
    //---------------Movement Variables---------------
    [SerializeField] float walkSpeed = 5.0f;
    [SerializeField] float runningSpeed = 10.0f;
    Vector2 input;
    KeyCode runKey = KeyCode.LeftShift;
    bool isRuning;
    bool isMoving;      //This one is for animations
    //----------------Jump Variables-----------------
    KeyCode jumpKey = KeyCode.Space;
    bool isGrounded;
    [SerializeField] float jumpForce = 15f;
    float coyoteTime = 0.1f;
    float coyoteTimeCounter;
    float jumpBufferTime = 0.1f;
    float jumpBufferCounter;
    float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;
    //----------------Vitals Variables----------------
    [SerializeField] float maxHealth = 10.0f;
    [SerializeField] float currentHealth;
    [SerializeField] float maxStamina = 100.0f;
    [SerializeField] float currentStamina;
    //----------------Attack Variables----------------
    bool canAttack = true;
    bool isAttacking = false;          //this reverts to false when the animation finishes
    [SerializeField] float atkDamage = 2.0f;
    [SerializeField] float attackDelay = 1.0f;
    float attackTimer;
    KeyCode atkKey = KeyCode.F;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float attackRange = 1.0f;
    
    void Start()
    {
        //Grab the componenets 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        //Set the current to max
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }
    
    void Update()
    {
        if (currentHealth <= 0)
            Death();
        float horizontal = Input.GetAxisRaw("Horizontal");
        input = new Vector2(horizontal, rb.velocity.y);
        isRuning = Input.GetKey(runKey);
        if (attackTimer > 0f  && !isAttacking)
            attackTimer -= Time.deltaTime;
        if(attackTimer <= 0f)
          {
              if (Input.GetKeyDown(atkKey))
              {
                  attackTimer = attackDelay;
                  Attack();
              }
          }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0f;
        }
        UpdateAnimations();          
    }
    
    void FixedUpdate()
    {
        float currentSpeed;
        if (isRuning && currentStamina > 0)
        {
            currentStamina -= Time.fixedDeltaTime;
            currentSpeed = runningSpeed;
        }
        else
        {
            if (currentStamina < maxStamina)
                {
                    currentStamina += Time.fixedDeltaTime;
                }
            currentSpeed = walkSpeed;
        }
        if(!isAttacking)
            rb.linearVelocity = currentSpeed * input;
        else
            rb.linearVelocity = new Vector2(0, rb.velocity.y);
    }
    
    void Death()
    {
        anim.SetBool("IsDead", true);
    }
    
    void OnDeathAnimationFinishes()    //this is triggered via the animation controller
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void TakeDamage(float amount)
    {
      currentHealth -= amount;
    }
    
    void Attack()
    {
        isAttacking = true;
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in enemiesHit)
        {
          if(enemy.TryGetComponent(out EnemyHandler enemyHandler))    //Here replace EnemyHandler for the name of your enemy script
              enemyHandler.TakeDamage(atkDamage);
        }   
    }
    //As i said in the top down movement, i'm not the best at animations, 
    void UpdateAnimations()
    {
        isMoving = input.sqrMagnitude > 0.001f;
        if (isMoving)
        {
          if (input.x > 0)
            sr.flipX = false;        //if the default animation is looking to the right don't flip, else change the false and true
          else
            sr.flipX = true;
        }
        bool runningAnim = isMoving && isRuning && isGrounded;
        anim.SetBool("IsMoving", isMoving);
        anim.SetBool("IsRunning", runningAnim);
        anim.SetBool("IsJumping", !isGrounded);
    }

}
