using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform laneBottom;
    public Transform laneTop;

    public float skyHeightOffset = 3f;
    public float spawnRate = 1.5f;
    private float timer = 0f;
    public float spawnX = 15f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnCoin();
            timer = 0f;
        }
    }

    void SpawnCoin()
    {
        int randomLane = Random.Range(0, 3);
        float spawnY = laneBottom.position.y;

        if (randomLane == 1)
        {
            spawnY = laneTop.position.y;
        }
        else if (randomLane == 2)
        {
            spawnY = laneTop.position.y + skyHeightOffset;
        }

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);
        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
    }
}