using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    // Audio
    public AudioClip projectedClip;
    public AudioClip enemyClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 100.0f) {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force) {
        rigidbody2d.AddForce(direction * force);
        
        if (projectedClip != null) {
            AudioSource.PlayClipAtPoint(projectedClip, transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null) {
            enemy.Fix();
            AudioSource.PlayClipAtPoint(enemyClip, transform.position);
        }
        Debug.Log("Projectile collision with " + other.gameObject);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
    
}
