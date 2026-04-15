using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // Input Actions
    // Character Movement
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    public float speed = 3.0f;
    Vector2 move;

    // Health System
    public int maxHealth = 5;
    int currentHealth;

    // Temporary Invincibility
    public float timeInvincible = 1.0f;
    bool isInvincible;
    float damageCooldown;

    // Animations
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        // Debug.Log(move);
        if(isInvincible) {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0) {
                isInvincible = false;
            }
        }
    }

    void FixedUpdate() {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount) {
       if (amount < 0) {
        if (isInvincible) {
            return;
        }
        isInvincible = true;
        damageCooldown = timeInvincible;
        animator.SetTrigger("Hit");
       }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    // Getters and Setters
    public int health { get { return currentHealth; }}
}
