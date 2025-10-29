using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] RawImage backgroundImage;
    [SerializeField] float _x, _y;

    private void Update()
    {
        // Moves the background image to create a parallax scrolling effects (not RawImage)
        backgroundImage.uvRect = new Rect(
            backgroundImage.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, backgroundImage.uvRect.size
        );

    }
}