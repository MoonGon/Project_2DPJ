using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float deadZone = -15f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}