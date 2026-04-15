using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip colectedClip;

    void OnTriggerEnter2D(Collider2D other) {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null && controller.health < controller.maxHealth) {
            controller.PlaySound(colectedClip);
            controller.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
}
