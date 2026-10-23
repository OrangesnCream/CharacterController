using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // stop the capsule tipping over
    }

    private void OnEnable() => moveAction.action.Enable();
    private void OnDisable() => moveAction.action.Disable();

    private void FixedUpdate()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // Relative to the player's own orientation
        Vector3 direction = transform.right * input.x + transform.forward * input.y;

        // Keep the current Y velocity so gravity still works
        Vector3 velocity = rb.linearVelocity;
        velocity.x = direction.x * moveSpeed;
        velocity.z = direction.z * moveSpeed;
        rb.linearVelocity = velocity;
    }
}
