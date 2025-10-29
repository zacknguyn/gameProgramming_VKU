using UnityEngine;

public class EnterGame : MonoBehaviour
{
    public Canvas GameMenu;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the game area.");
            GameMenu.enabled = true;
        }
    }
}
