using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas GameMenu;
    private bool isShowingMenu = false;

    public TMPro.TMP_Text Title;
    public TMPro.TMP_Text Message;

    public Button ContinueButton;
    public Button RestartButton;

    private void Start()
    {
        GameMenu.enabled = isShowingMenu;
        Title.text = "Welcome to the Game!";
        Message.text = "Do you want to continue or restart?";

        ContinueButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Continue";
        RestartButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Restart";
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Scene3.2");
    }
}
