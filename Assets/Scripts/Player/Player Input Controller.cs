using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public event Action<float> horizontalMovement;
    public event Action OnJumpPressed;
    public event Action OnJumpReleased;
    public event Action OnAttackPressed;
    public event Action OnAttackReleased;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;

        inputActions.Player.Jump.performed += OnJumpPerformed;
        inputActions.Player.Jump.canceled += OnJumpCanceled;

        inputActions.Player.Attack.performed += OnAttackPerformed;
        inputActions.Player.Attack.canceled += OnAttackCanceled;

        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;

        inputActions.Player.Jump.performed -= OnJumpPerformed;
        inputActions.Player.Jump.canceled -= OnJumpCanceled;

        inputActions.Player.Attack.performed -= OnAttackPerformed;
        inputActions.Player.Attack.canceled -= OnAttackCanceled;

        inputActions.Disable();
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        horizontalMovement?.Invoke(context.ReadValue<Vector2>().x);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        horizontalMovement?.Invoke(context.ReadValue<Vector2>().x);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJumpPressed?.Invoke();
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        OnJumpReleased?.Invoke();
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnAttackPressed?.Invoke();
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        OnAttackReleased?.Invoke();
    }
}
