using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform laneTop;
    public Transform laneBottom;

    [Header("Difficulty Settings")]
    public float currentSpawnRate = 2.5f;
    public float minSpawnRate = 0.6f;
    public float difficultyRamp = 0.05f;

    private float timer = 0f;
    public float spawnX = 15f;

    void Update()
    {
        if (currentSpawnRate > minSpawnRate)
        {
            currentSpawnRate -= difficultyRamp * Time.deltaTime;
        }

        timer += Time.deltaTime;
        if (timer >= currentSpawnRate)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        bool isTop = Random.value > 0.5f;
        float spawnY = isTop ? laneTop.position.y : laneBottom.position.y;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}