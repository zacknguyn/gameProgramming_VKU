using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    /* SETTINGS */
    [SerializeField] Canvas PauseCanva;
    [SerializeField] Canvas GameOverCanva;
    public ParticleSystem explosion;
    public Player player;
    public int lives = 3;
    public int score = 0;
    public float invulnerabilityTime = 3f;
    public float respawnTime = 3f;

    UIController uiController;
    AsteroidSpawner asteroidSpawner;
    StarSpawner starSpawner;

    private void Start()
    {
        uiController = FindObjectOfType<UIController>();
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
        starSpawner = FindObjectOfType<StarSpawner>();

        this.PauseCanva.enabled = false;
        this.GameOverCanva.enabled = false;
        asteroidSpawner.Initialize();
        starSpawner.Initialize();

        StartCoroutine(InitializeUI()); // Load UI after one frame
    }

    private IEnumerator InitializeUI()
    {
        yield return null; // Wait one frame
        this.uiController.UpdateLives();
        this.uiController.UpdateScore();
    }

    public void StarCollected()
    {
        this.score += 200;
        this.uiController.UpdateScore();
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        // TODO: Increase score based on asteroid size
        if (asteroid.size < .75f)
        {
            this.score += 100;
        }
        else if (asteroid.size < 1.2f)
        {
            this.score += 50;
        }
        else
        {
            this.score += 25;
        }

        this.uiController.UpdateScore();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;
        this.uiController.UpdateLives();

        if (this.lives <= 0)
        {
            GameOver();
        } else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }
    
    public void TogglePause_GameOver()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            this.GameOverCanva.enabled = true;
        }
        else
        {
            Time.timeScale = 1f;
            this.GameOverCanva.enabled = false;
        }
    }

    public void TogglePause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            asteroidSpawner.Deinitialize();
            starSpawner.StopSpawning();
            this.PauseCanva.enabled = true;
        }
        else
        {
            Time.timeScale = 1f;
            asteroidSpawner.Initialize();
            starSpawner.Initialize();
            this.PauseCanva.enabled = false;
        }
    }

    private void Respawn()
    {
        this.player.gameObject.transform.position = Vector3.zero;
        this.player.gameObject.layer = 10; // Set to IgnoreCollisions layer
        this.player.gameObject.SetActive(true);

        StartCoroutine(Blink());
        Invoke(nameof(TurnOnCollisions), this.invulnerabilityTime);
    }

    private IEnumerator Blink()
    {
        SpriteRenderer sprite = this.player.gameObject.GetComponent<SpriteRenderer>();
        Color defaultColor = sprite.color;

        for (int i = 0; i < 7; i++) // Blinks 7 times
        {
            //sprite.color = new Color(1f, 1f, 1f, 0.5f);
            sprite.enabled = false;
            yield return new WaitForSeconds(.3f);
            sprite.enabled = true;
            //sprite.color = defaultColor;
            yield return new WaitForSeconds(.15f);
        }
    }

    private void TurnOnCollisions()
    {
        // Set to layer "Player"
        this.player.gameObject.layer = 6;
    }

    private void GameOver()
    {
        TogglePause_GameOver();

        this.lives = 3;
        this.score = 0;

        this.uiController.UpdateLives();
        Invoke(nameof(Respawn), this.respawnTime);
    }
}
