using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 5f;
    private float backgroundWidth;
    public Transform otherBackground;

    void Start()
    {
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (transform.position.x <= -backgroundWidth)
        {
            Vector3 newPos = otherBackground.position;
            newPos.x += backgroundWidth;
            transform.position = newPos;
        }
    }
}