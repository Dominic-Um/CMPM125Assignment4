using UnityEngine;
using NewerInput;
using UnityEngine.InputSystem;
using System;

[CreateAssetMenu(menuName = "Scriptable Objects/ Input Reader")]
public class InputReader : ScriptableObject, CustomInput.IPlayerActions, CustomInput.IDetectiveActions
{
    public CustomInput customInput;

    private void OnEnable()
    {
        if (customInput == null)
        {
            customInput = new CustomInput();
            customInput.Player.SetCallbacks(this);
            customInput.Detective.SetCallbacks(this);
        }

        SetGameplay();
    }

    private void OnDisable()
    {
        DisableAll();
    }

    //platforming controls
    public void SetGameplay()
    {
        DisableAll();
        customInput.Player.Enable();
    }

    //detective controls
    public void SetDetective()
    {
        DisableAll();
        customInput.Detective.Enable();
    }

    public void DisableAll()
    {
        customInput.Player.Disable();
        customInput.Detective.Disable();
    }

    //PLATFORMING
    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;
    public event Action DashEvent;
    public event Action Ability1Event;
    public event Action Ability2Event;

    //DETECTIVE
    public event Action<Vector2> PanEvent;
    public event Action MarkEvent;
    public event Action UndoMarkEvent;
    public event Action ToggleEvent;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            DashEvent?.Invoke();
        }
    }

    public void OnAbility1(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Ability1Event?.Invoke();
        }
    }

    public void OnPan(InputAction.CallbackContext context)
    {
        PanEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMark(InputAction.CallbackContext context)
    { 
        if (context.phase == InputActionPhase.Performed)
        {
            MarkEvent?.Invoke();
        }
    }

    public void OnUndoMark(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            UndoMarkEvent?.Invoke();
        }
    }

    public void OnAbility2(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Ability2Event?.Invoke();
        }
    }

    public void OnToggle(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ToggleEvent?.Invoke();
        }
    }
}
