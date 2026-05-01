using UnityEngine;

public class BulletHit : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}