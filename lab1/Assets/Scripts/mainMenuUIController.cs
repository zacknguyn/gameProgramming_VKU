using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuUIController : MonoBehaviour
{
    
    public void NewGameClicked()
    {
        // TODO: Implement new game logic
        SceneManager.LoadScene("playScene");
    }

    public void ShopClicked()
    {
        // TODO: Implement shop logic
        SceneManager.LoadScene("shopScene");
    }

    public void ContinueClicked()
    {
        // TODO: Implement continue game logic
        SceneManager.LoadScene("playScene");
    }

    public void ExitClicked()
    {
        // TODO: Implement exit game logic
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
