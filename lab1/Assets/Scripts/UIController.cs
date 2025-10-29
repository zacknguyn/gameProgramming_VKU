using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;

    GameManager gameManager;

    private void Start()
    {
        // TODO: Finds the GameManager instance in the scene
        gameManager = FindObjectOfType<GameManager>();
    }
    
    public void NewGameClicked()
    {
        gameManager.TogglePause_GameOver(); // Unpause the game
        SceneManager.LoadScene("playScene");
    }
    public void ResumeClicked()
    {
        gameManager.TogglePause();
    }

    public void MainMenuClicked()
    {
        gameManager.TogglePause();
        SceneManager.LoadScene("mainMenuScene");
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + this.gameManager.score.ToString();
    }

    public void UpdateLives()
    {
        Debug.Log("Updating lives UI: " + this.gameManager.lives);
        livesText.text = "Lives: " + this.gameManager.lives.ToString();
    }
}
