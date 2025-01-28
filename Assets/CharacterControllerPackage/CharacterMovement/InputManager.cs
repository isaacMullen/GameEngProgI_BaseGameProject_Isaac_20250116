using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, InputActions.IPlayerActions
{
    //Referencing the inputActions required to integrate with the new InputSystem
    public InputActions inputActions;

    //Initializing, enabling and setting the callbacks to this class
    void Start()
    {
        inputActions = new InputActions();
        inputActions.Player.SetCallbacks(this);

        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }
    
    //Movement Action from IPlayerActions
    public void OnMove(InputAction.CallbackContext context)
    {
        //Checking if the inputAction is performed
        if (context.performed)
        {
            Actions.MoveEvent?.Invoke(context.ReadValue<Vector2>());
            Debug.Log(context.ReadValue<Vector2>());
        }       
        //Checking if it is cancelled
        if (context.canceled)
        {
            //Sending a zerod out vector2 to stop the player from moving when button is lifted up
            Actions.MoveEvent?.Invoke(Vector2.zero);
        }
    }

    //Change environment action
    public void OnChangeEnvironment(InputAction.CallbackContext context)
    {
        //When the button is first pressed
        if (context.started)
        {
            //Sending bools to the respective 'update' methods as a way to toggle the main methods in unity's Update method
            Actions.ChangeEnvironmentTextEvent?.Invoke(true);
            Actions.ChangePlanetSizeEvent?.Invoke(true);
        }
        //Cancelling the methods by sending false flags
        if (context.canceled)
        {
            Actions.ChangeEnvironmentEvent?.Invoke();
            Actions.ChangeEnvironmentTextEvent?.Invoke(false);
            Actions.ChangePlanetSizeEvent?.Invoke(false);
        }
        
    }
}

//To hold actions
public static class Actions
{
    public static Action<Vector2> MoveEvent;
    public static Action ChangeEnvironmentEvent;
    public static Action<bool> ChangeEnvironmentTextEvent;
    public static Action<bool> ChangePlanetSizeEvent;
}
