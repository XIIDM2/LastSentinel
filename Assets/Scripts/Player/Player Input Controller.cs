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

    private InputAction jumpInput;
    private InputAction attackInput;

    private void Awake()
    {
        jumpInput = InputSystem.actions.FindAction("Jump");
        attackInput = InputSystem.actions.FindAction("Attack");
    }

    private void OnEnable()
    {

        jumpInput.performed += OnJumpPerformed;
        jumpInput.canceled += OnJumpCanceled;

        attackInput.performed += OnAttackPerformed;
        attackInput.canceled += OnAttackCanceled;
    }

    private void OnDisable()
    {
        jumpInput.performed -= OnJumpPerformed;
        jumpInput.canceled -= OnJumpCanceled;

        attackInput.performed -= OnAttackPerformed;
        attackInput.canceled -= OnAttackCanceled;
    }
    public void OnMove(InputValue value)
    {
        horizontalMovement?.Invoke(value.Get<Vector2>().x);
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
