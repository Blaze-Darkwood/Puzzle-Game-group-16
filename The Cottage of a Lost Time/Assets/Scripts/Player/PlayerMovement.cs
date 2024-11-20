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
    private float originalDrag;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalDrag = rb.linearDamping;
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

        // Ground check for drag (linearDamping)
        if (GroundCheck())
            rb.linearDamping = originalDrag;
        else
            rb.linearDamping = 0;
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

    private void OnJump(InputValue inp) // Make character jump
    {
        if (GroundCheck())
        {
            rb.linearDamping = 0;
            rb.AddForce(new(0, jumpForce));
        }
    }

    private bool GroundCheck() // Check if we touch ground
    {
        Vector3 pos = transform.position;
        Vector3 scale = transform.localScale;
        pos.y -= transform.lossyScale.y / 2;
        scale.y = .2f;

        return Physics.BoxCast(pos, scale, Vector3.down, Quaternion.identity, .5f);
    }
}
