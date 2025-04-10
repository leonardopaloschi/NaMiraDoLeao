using UnityEngine;

public class TriangleSpawner : MonoBehaviour
{
    public GameObject trianglePrefab;
    public float spawnInterval = 2f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public Transform player; 

    private float timer;
    private int coinsCollected = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTriangle();
            timer = 0f;
        }
    }

    void SpawnTriangle()
    {
        Vector2 randomPos;
        float distanceToPlayer;
        bool positionOk;

        int maxAttempts = 20;
        int attempts = 0;

        do
        {
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            randomPos = new Vector2(x, y);
            distanceToPlayer = Vector2.Distance(randomPos, player.position);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPos, 1f);
            positionOk = true;

            foreach (var col in colliders)
            {
                if (col.CompareTag("Coletavel"))
                {
                    positionOk = false;
                    break;
                }
            }

            attempts++;

        } while ((distanceToPlayer < 1.5f || !positionOk) && attempts < maxAttempts);

        if (attempts < maxAttempts)
        {
            Instantiate(trianglePrefab, randomPos, Quaternion.identity);
        }
    }

    public void OnTriangleCollected()
    {
        coinsCollected++;

        if (coinsCollected % 10 == 0)
        {
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.2f); 
        }
    }
}
