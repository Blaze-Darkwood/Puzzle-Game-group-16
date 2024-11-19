using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Transform cam;

    private Rigidbody rb;
    private PlayerInputs input;
    private Vector3 move = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable() // Enable input actions
    {
        input.Enable();
    }

    private void OnDisable() // Disable input actions
    {
        input.Disable();
    }

    private void Update()
    {
        // Movement
        float speed = .5f;

        if (rb.linearVelocity.magnitude + move.magnitude < maxSpeed)
            speed = moveSpeed;

        rb.AddRelativeForce(2 * speed * move);
    }

    private void OnMove(InputValue inp) // Store movement input
    {
        move = inp.Get<Vector3>();
    }

    private void OnLook(InputValue inp) // Change camera/player direction
    {
        Vector2 delta = inp.Get<Vector2>();
        float dTime = Time.deltaTime * 20;

        cam.Rotate(Mathf.Clamp(delta.y * -dTime, -85, 85), 0, 0);
        transform.Rotate(0, delta.x * dTime, 0);
    }
}
