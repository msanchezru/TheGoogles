using UnityEngine;
using UnityEngine.InputSystem;

public class PitchController : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 0.1f;

    private const float MinPitch = -60f;
    private const float MaxPitch = 60f;

    private float yaw;
    private float pitch;

    private void Awake()
    {
        Vector3 angles = transform.localEulerAngles;
        yaw = angles.y;
        pitch = Mathf.Clamp(NormalizeAngle(angles.x), MinPitch, MaxPitch);
    }

    private void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        yaw += mouseDelta.x * _sensitivity;
        pitch = Mathf.Clamp(pitch - mouseDelta.y * _sensitivity, MinPitch, MaxPitch);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private static float NormalizeAngle(float angle)
    {
        return angle > 180f ? angle - 360f : angle;
    }
}
