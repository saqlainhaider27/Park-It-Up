using System;
using UnityEngine;

public class InputManager : Singleton<InputManager> {

    private InputSystem_Actions inputActions;

    public event EventHandler OnSprintKeyHold;
    public event EventHandler OnSprintKeyReleased;

    private void Awake() {
        inputActions = new InputSystem_Actions();
    }

    private void Start() {
        inputActions.Player.Sprint.performed += Sprint_performed;
        inputActions.Player.Sprint.canceled += Sprint_canceled;
    }

    private void Sprint_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnSprintKeyReleased?.Invoke(this, EventArgs.Empty);
    }

    private void Sprint_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnSprintKeyHold?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVector() {
        return inputActions.Player.Move.ReadValue<Vector2>();
    }
    private void OnEnable() {
        inputActions.Enable();
    }
    private void OnDisable() {
        inputActions.Disable();
    }
}

