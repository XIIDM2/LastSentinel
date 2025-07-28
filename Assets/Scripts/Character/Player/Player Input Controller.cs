using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputController : MonoBehaviour
{
    public event Action<float> horizontalInputValue;
    public event Action OnJumpPressed;
    public event Action OnAttackPressed;
    public event Action OnAttackReleased;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput.notificationBehavior != PlayerNotifications.InvokeCSharpEvents)
        {
            Debug.LogError("Wrong Player Input Behavior for onActionTriggerEvents, please switch to 'Invoke C# Events' ");
        }
    }

    private void OnEnable()
    {
        playerInput.onActionTriggered += HandleAction;
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= HandleAction;
    }

    private void HandleAction(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":
                horizontalInputValue?.Invoke(context.ReadValue<Vector2>().x);
                break;
            case "Jump":
                if (context.started)
                {
                    OnJumpPressed?.Invoke();
                }
                break;
            case "Attack":
                if (context.started)
                {
                    OnAttackPressed?.Invoke();
                }
                else if (context.canceled)
                {
                    OnAttackReleased?.Invoke();
                }
                break;
        }
    }

    #region C# InputSystemActions Script
    //private InputSystem_Actions inputActions;

    //private void Awake()
    //{
    //    inputActions = new InputSystem_Actions();
    //}

    //private void OnEnable()
    //{
    //    inputActions.Player.Move.performed += OnMovePerformed;
    //    inputActions.Player.Move.canceled += OnMoveCanceled;

    //    inputActions.Player.Jump.performed += OnJumpPerformed;
    //    inputActions.Player.Jump.canceled += OnJumpCanceled;

    //    inputActions.Player.Attack.performed += OnAttackPerformed;
    //    inputActions.Player.Attack.canceled += OnAttackCanceled;

    //    inputActions.Enable();

    //}

    //private void OnDisable()
    //{
    //    inputActions.Player.Move.performed -= OnMovePerformed;
    //    inputActions.Player.Move.canceled -= OnMoveCanceled;

    //    inputActions.Player.Jump.performed -= OnJumpPerformed;
    //    inputActions.Player.Jump.canceled -= OnJumpCanceled;

    //    inputActions.Player.Attack.performed -= OnAttackPerformed;
    //    inputActions.Player.Attack.canceled -= OnAttackCanceled;

    //    inputActions.Disable();
    //}
    //private void OnMovePerformed(InputAction.CallbackContext context)
    //{
    //    horizontalMovement?.Invoke(context.ReadValue<Vector2>().x);
    //}

    //private void OnMoveCanceled(InputAction.CallbackContext context)
    //{
    //    horizontalMovement?.Invoke(context.ReadValue<Vector2>().x);
    //}

    //private void OnJumpPerformed(InputAction.CallbackContext context)
    //{
    //    OnJumpPressed?.Invoke();
    //}

    //private void OnJumpCanceled(InputAction.CallbackContext context)
    //{
    //    OnJumpReleased?.Invoke();
    //}

    //private void OnAttackPerformed(InputAction.CallbackContext context)
    //{
    //    OnAttackPressed?.Invoke();
    //}

    //private void OnAttackCanceled(InputAction.CallbackContext context)
    //{
    //    OnAttackReleased?.Invoke();
    //}
    #endregion
}
