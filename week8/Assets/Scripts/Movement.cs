using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 5f;

    private PlayerInputActions playerControls;
    private InputAction moveAction;
    private Vector2 currentMoveInput;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        moveAction = playerControls.Player.Move;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        currentMoveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        currentMoveInput = Vector2.zero;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = new Vector3(currentMoveInput.x, 0, currentMoveInput.y);

        if (movementDirection.magnitude > 1f)
        {
            movementDirection.Normalize();
        }

        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

}
