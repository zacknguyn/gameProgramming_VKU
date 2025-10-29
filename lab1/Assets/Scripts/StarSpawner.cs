using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [Header("Star Settings")]
    public GameObject star;
    public BoxCollider2D starSpawner;

    [Header("Spawn Settings")]
    public float minSpawnDelay = 5f;
    public float maxSpawnDelay = 10f;

    private float currentSpawnDelay;
    private bool isSpawning = false;

    public void Initialize()
    {
        if (isSpawning) return; // Prevent multiple InvokeRepeating calls
        isSpawning = true;

        SetNextSpawnDelay();
        InvokeRepeating(nameof(SpawnStar), currentSpawnDelay, currentSpawnDelay);
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke(nameof(SpawnStar));
    }

    private void SpawnStar()
    {
        if (star == null) return;

        Vector2 randomPos = GetRandomPosition();
        Instantiate(star, randomPos, Quaternion.identity);

        // Randomize next spawn delay dynamically
        SetNextSpawnDelay();

        // Update the repeat rate (Unity doesn't allow changing InvokeRepeating rate directly)
        CancelInvoke(nameof(SpawnStar));
        InvokeRepeating(nameof(SpawnStar), currentSpawnDelay, currentSpawnDelay);
    }

    private Vector2 GetRandomPosition()
    {
        if (starSpawner != null)
        {
            var bounds = starSpawner.bounds;
            return new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );
        }

        // Default range if no BoxCollider2D is set
        return new Vector2(
            Random.Range(-8f, 8f),
            Random.Range(-4.5f, 4.5f)
        );
    }

    private void SetNextSpawnDelay()
    {
        currentSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
    }
}
