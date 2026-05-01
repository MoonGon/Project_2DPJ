using UnityEngine;

public class CoinMove : MonoBehaviour
{
    public float moveSpeed = 8f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}