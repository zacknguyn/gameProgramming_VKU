using UnityEngine;

public class Star : MonoBehaviour
{

    public float maxLifetime = 10f;

    private void Start()
    {
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().StarCollected();
            Destroy(this.gameObject);
        }
    }
}
