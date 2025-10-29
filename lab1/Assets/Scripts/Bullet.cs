using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    /* SETTINGS */
    public float projectileSpeed = 520f;
    public float maxLifetime = 10f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.projectileSpeed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    // Detect collision with other objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }


}

