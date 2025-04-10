using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 4f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public Transform player;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomPos;
        float distanceToPlayer;
        int attempts = 0;
        const int maxAttempts = 20;

        do
        {
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            randomPos = new Vector2(x, y);
            distanceToPlayer = Vector2.Distance(randomPos, player.position);
            attempts++;
        } while (distanceToPlayer < 4.0f && attempts < maxAttempts);

        if (attempts < maxAttempts)
        {
            GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            enemy.GetComponent<EnemyBehavior>()?.SetTarget(player);
        }
    }
}
