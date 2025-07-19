using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Action<float> horizontalMovement;
    public Action Jump;

    public void OnMove(InputValue value)
    {
        horizontalMovement?.Invoke(value.Get<Vector2>().x);
    }

    public void OnJump(InputValue value)
    {
        Jump?.Invoke();
    }
}
