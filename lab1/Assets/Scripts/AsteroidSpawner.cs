using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    /* SETTINGS */
    public Asteroid asteroidPrefab;
    public float spawnRate = 2f; // Asteroids per second
    public float spawnDistance = 20f;
    public int spawnAmount = 1;
    public float trajectoryVariance = 15f; // Degrees

    public void Initialize()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    public void Deinitialize()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            float randomZ = Random.Range(1f, 10f);
            Vector2 spawnDirection2D = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnDirection = new Vector3(spawnDirection2D.x, spawnDirection2D.y, randomZ);
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            // Quaternion is a representation of rotation
            // Create a rotation that adds some random variance to the asteroid's trajectory
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
