using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Transform cam;

    private Rigidbody rb;
    private PlayerInputs input;
    private Vector2 move = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        // Movement
        float speed = .5f;

        if (rb.linearVelocity.magnitude + move.magnitude > maxSpeed)
            speed = moveSpeed;

        rb.AddRelativeForce(2 * speed * move);

        // Ground check
    }

    private void OnMove()
    {
        // Store movement input
        move = input.Player.Move.ReadValue<Vector2>();
    }
}
