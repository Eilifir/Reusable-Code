using UnityEngine;
//Quick disclaimer all of the values are selected at random, feel free to tune them to your heart's content
//Also your player need to have the components listed in References + an empty GameObject called "attackPoint"
public class TopDownMovement : MonoBehaviour
{
    //-------------------References-------------------
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    //---------------Movement Variables---------------
    [SerializeField] float walkSpeed = 5.0f;
    [SerializeField] float runningSpeed = 10.0f;
    Vector2 input;
    KeyCode runKey = KeyCode.LeftShift;
    bool isRuning;
    bool isMoving;      //This one is for animations
    float lastX = 0f;
    float lastY = -1f;
    //----------------Vitals Variables----------------
    [SerializeField] float maxHealth = 10.0f;
    [SerializeField] float currentHealth;
    [SerializeField] float maxStamina = 100.0f;
    [SerializeField] float currentStamina;
    //----------------Attack Variables----------------
    bool canAttack = true;
    bool isAttacking = false;            //this reverts to false when the animation finishes
    [SerializeField] float atkDamage = 2.0f;
    [SerializeField] float attackDelay = 1.0f;
    float attackTimer;
    KeyCode atkKey = KeyCode.F;
    Transform attackPoint;        //You can put a [SerializeField] and drag it from the inspector
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float attackRange = 1.0f;
    
    void Start()
    {
        //Grab the componenets 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        attackPoint = transform.Find("attackPoint");       //If you drag it from the inspector delete this :)
        //Set the current to max
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        input = new Vector2(horizontal, vertical).normalized;
        isRuning = Input.GetKey(runKey);
        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;
         if(attackTimer <= 0f)
          {
              if (Input.GetKeyDown(atkKey))
              {
                  attackTimer = attackDelay;
                  Attack();
              }
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
            rb.linearVelocity = Vector2.zero;
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
    //Here it can get messy (i'm not the best at animations) but basically i have an animation tree with idle/walk/run/attack animations in 8 directions with moveX and moveY to see where the player is moving in a
    //cartesian plane, and two boolens to see if it's moving and running.
    void UpdateAnimations()
    {
        isMoving = input.sqrMagnitude > 0.001f;
        if (isMoving && !isAttacking)
        {
        lastX = input.x;
        lastY = input.y;
        }
        bool runningAnim = isMoving && isRuning;
        anim.SetBool("IsMoving", isMoving);
        anim.SetBool("IsRunning", runningAnim);
        anim.SetBool("IsAttacking", isAttacking);
        anim.SetFloat("MoveX", lastX);
        anim.SetFloat("MoveY", lastY);
    }

}
