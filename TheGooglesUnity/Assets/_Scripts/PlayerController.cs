using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        Vector2 input = new Vector2(
            (keyboard.dKey.isPressed ? 1f : 0f) - (keyboard.aKey.isPressed ? 1f : 0f),
            (keyboard.wKey.isPressed ? 1f : 0f) - (keyboard.sKey.isPressed ? 1f : 0f));
        float verticalInput =
            (keyboard.spaceKey.isPressed ? 1f : 0f) -
            (keyboard.leftCtrlKey.isPressed ? 1f : 0f);

        Vector3 movement = transform.right * input.x
            + transform.forward * input.y
            + Vector3.up * verticalInput;
        movement = Vector3.ClampMagnitude(movement, 1f);

        _characterController.Move(movement * _moveSpeed * Time.deltaTime);
    }
}
