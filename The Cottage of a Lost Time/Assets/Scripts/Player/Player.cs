using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask crystalLayer;

    private Rigidbody rb;
    private PlayerInputs input;
    private Vector3 move = Vector3.zero;
    private float originalDrag;
    private float xRotation = .0f;
    private bool crystalMove;
    private Transform selectedCrystal;

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
        // Limit speed
        float _speed = .5f;

        if (rb.linearVelocity.magnitude + move.magnitude < maxSpeed)
            _speed = moveSpeed;

        rb.AddRelativeForce(2 * _speed * move);

        // Ground check for drag (now linearDamping)
        if (GroundCheck())
            rb.linearDamping = originalDrag;
        else
            rb.linearDamping = 0;
    }

    private void OnMove(InputValue _inp) // Store movement input
    {
        move = _inp.Get<Vector3>();
    }

    private void OnLook(InputValue _inp) // Change camera/player direction
    {
        Vector2 _delta = _inp.Get<Vector2>();
        float _dTime = Time.deltaTime * 20;
        float _rotX = -_delta.y * _dTime;
        xRotation = Mathf.Clamp(_rotX + xRotation, -85, 85);

        if (!crystalMove)
        {
            cam.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(0, _delta.x * _dTime, 0);
        }
        else
            selectedCrystal.Rotate(0, _delta.x * _dTime, 0);
    }

    private void OnJump() // Make character jump
    {
        if (GroundCheck())
        {
            rb.linearDamping = 0;
            rb.AddForce(new(0, jumpForce));
        }
    }

    private void OnCrystal() // Go into crystal direction change mode
    {
        if (!crystalMove)
        {
            Debug.Log("sending raycast...");
            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, 5))
            {
                Debug.Log("selected '" + hit.collider.name + "'");
                selectedCrystal = hit.collider.transform;
                crystalMove = true;
            }
        }
        else crystalMove = false;
    }

    private bool GroundCheck() // Check if we touch ground
    {
        Vector3 _pos = transform.position;
        Vector3 _scale = transform.localScale;
        _pos.y -= transform.lossyScale.y / 2;
        _scale.y = .2f;

        return Physics.BoxCast(_pos, _scale, Vector3.down, Quaternion.identity, .5f);
    }
}
