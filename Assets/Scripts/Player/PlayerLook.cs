using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private float sensitivity = 0.1f;

    [Tooltip("Camera or pivot that tilts up/down. Leave empty for yaw-only.")]
    [SerializeField] private Transform pitchTarget;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;
    [SerializeField] private bool lockCursor = true;

    private float pitch;

    private void OnEnable()
    {
        lookAction.action.Enable();

        if (lockCursor)
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        lookAction.action.Disable();

        if (lockCursor)
            Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        Vector2 look = lookAction.action.ReadValue<Vector2>() * sensitivity;

        // Yaw: turn the whole player left/right
        transform.Rotate(0f, look.x, 0f);

        // Pitch: tilt only the camera/pivot up/down
        if (pitchTarget != null)
        {
            pitch = Mathf.Clamp(pitch - look.y, minPitch, maxPitch);
            pitchTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }
}
