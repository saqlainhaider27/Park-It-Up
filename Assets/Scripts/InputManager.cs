using UnityEngine;

public class InputManager : Singleton<InputManager> {

    private InputSystem_Actions inputActions;

    private void Awake() {
        inputActions = new InputSystem_Actions();
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

