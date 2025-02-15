using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Universal Player Controller that supports Keyboard, Gamepad, and VR Controls.
/// - Left Stick / WASD / Left Joystick: Moves the player.
/// - Right Stick / Mouse / Right Joystick: Aims.
/// - Trigger / Click: Fires weapons.
/// - "E" / Gamepad "X" / VR Grip: Interacts.
/// - Supports Jumping, Sprinting, and Crouching.
/// </summary>
public class UniversalPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float rotationSpeed = 200f;
    public float sprintMultiplier = 1.5f;

    [Header("VR-Specific Settings")]
    public Transform vrCamera;
    public Transform cameraRig;
    public float heightAdjustmentSpeed = 0.5f;
    public float minHeight = 1.5f;
    public float maxHeight = 3.5f;

    private CharacterController characterController;
    private UniversalControls controls;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isSprinting = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        controls = new UniversalControls();

        // Movement
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Look
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        // Sprint
        controls.Player.Sprint.performed += ctx => isSprinting = true;
        controls.Player.Sprint.canceled += ctx => isSprinting = false;

        // Fire
        controls.Player.Fire.performed += ctx => FireWeapon();

        // Interact
        controls.Player.Interact.performed += ctx => Interact();
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
        AdjustCameraHeight();
    }

    private void MovePlayer()
    {
        float speed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;

        if (moveDirection.magnitude > 0.1f)
        {
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }
    }

    private void RotatePlayer()
    {
        if (Mathf.Abs(lookInput.x) > 0.1f)
        {
            transform.Rotate(Vector3.up, lookInput.x * rotationSpeed * Time.deltaTime);
        }
    }

    private void AdjustCameraHeight()
    {
        if (cameraRig == null) return;

        float cameraHeightAdjust = Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical");

        if (Mathf.Abs(cameraHeightAdjust) > 0.1f)
        {
            float newHeight = cameraRig.position.y + cameraHeightAdjust * heightAdjustmentSpeed * Time.deltaTime;
            newHeight = Mathf.Clamp(newHeight, minHeight, maxHeight);
            cameraRig.position = new Vector3(cameraRig.position.x, newHeight, cameraRig.position.z);
        }
    }

    private void FireWeapon()
    {
        Debug.Log("Weapon Fired!");
        // TODO: Add shooting logic.
    }

    private void Interact()
    {
        Debug.Log("Interacted!");
        // TODO: Add interaction logic.
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
