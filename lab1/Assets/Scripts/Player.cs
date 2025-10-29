using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private bool _thrusting;
    private float _turnDirection;

    // THINGS RELATED TO SHIP'S LEANING ANIMATION
    private bool isLeaning;
    private int spriteIndex;

    /* SETTINGS */
    public Sprite[] shipSprites;
    public Bullet bulletPrefab;
    public float thrustForce = 1f;
    public float turnForce = .1f;

    /* KEY SETTINGS */
    private KeyCode forward = KeyCode.UpArrow;
    private KeyCode left = KeyCode.LeftArrow;
    private KeyCode right = KeyCode.RightArrow;
    private KeyCode shoot = KeyCode.Space;
    private KeyCode pause = KeyCode.Escape;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(ShipLeaning), 1f / 12f, 1f / 12f);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(ShipLeaning));
    }
    private void Update()
    {
        _thrusting = Input.GetKey(forward);

        if (Input.GetKey(left))
        {
            isLeaning = true;
            _spriteRenderer.flipX = false;
            _turnDirection = 1f;
        }
        else if (Input.GetKey(right))
        {
            isLeaning = true;
            _spriteRenderer.flipX = true;
            _turnDirection = -1f;
        }
        else
        {
            isLeaning = false;
            _turnDirection = 0f;
        }

        if (Input.GetKeyDown(shoot))
        {
            Shoot();
        }
        
        if (Input.GetKeyDown(pause))
        {
            FindFirstObjectByType<GameManager>().TogglePause();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Handle thrusting
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustForce);
        }

        // Handle turning
        if (_turnDirection != 0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnForce);
        }
    }

    private void Shoot()
    {
        Bullet newBullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);

        newBullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            // Reset player position and velocity
            _rigidbody.angularVelocity = 0f;
            _rigidbody.linearVelocity = Vector2.zero;

            this.gameObject.SetActive(false);

            FindFirstObjectByType<GameManager>().PlayerDied();
        }
    }

    private void ShipLeaning()
    {
        if (isLeaning)
        {
            spriteIndex++;

            if(spriteIndex >= shipSprites.Length)
            {
                spriteIndex = shipSprites.Length - 1;
            }

            _spriteRenderer.sprite = shipSprites[spriteIndex];
        } else
        {
            spriteIndex--;
            if (spriteIndex <= 0)
            {
                spriteIndex = 0;
            }
            _spriteRenderer.sprite = shipSprites[spriteIndex];
        }
    }
}
