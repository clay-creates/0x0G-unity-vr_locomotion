using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private UniversalControls controls;
    private Vector2 moveInput;
    private Vector2 lookInput;

    private void Awake()
    {
        controls = new UniversalControls();

        // Movement
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Look / Aim
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        // Fire / Attack
        controls.Player.Fire.performed += ctx => Fire();

        // Interact
        controls.Player.Interact.performed += ctx => Interact();
    }

    private void Fire()
    {
        Debug.Log("Firing weapon!");
        // Add weapon firing logic
    }

    private void Interact()
    {
        Debug.Log("Interacting!");
        // Add interaction logic
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
