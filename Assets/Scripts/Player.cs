using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : Singleton<Player> {

    private CharacterController _player;

    [SerializeField] private float _idleSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private float _speedTransitionTime = 0.1f; // Time to transition between speeds

    private IInteractable _interactable;

    public float Speed {
        get; private set;
    }

    private PlayerStates _currentState;
    private float _currentVelocity;

    public PlayerStates CurrentState {
        get {
            return _currentState;
        }
        private set {
            _currentState = value;
            SetTargetSpeed();
        }
    }

    private float _targetSpeed; // New target speed to interpolate toward
    [SerializeField] private LayerMask _collisionLayer;

    private void Awake() {
        _player = GetComponent<CharacterController>();
    }

    private void Start() {
        InputManager.Instance.OnSprintKeyHold += InputManager_OnSprintKeyHold;
        InputManager.Instance.OnSprintKeyReleased += InputManager_OnSprintKeyReleased;
        InputManager.Instance.OnInteractKeyPressed += InputManager_OnInteractKeyPressed;
    }

    private void InputManager_OnInteractKeyPressed(object sender, EventArgs e) {
        Debug.Log("Interaction Performed");
        _interactable?.Interact();
    }
    private void InputManager_OnSprintKeyHold(object sender, EventArgs e) {
        if (CurrentState == PlayerStates.Walking) {
            CurrentState = PlayerStates.Running;
        }
    }

    private void InputManager_OnSprintKeyReleased(object sender, EventArgs e) {
        if (CurrentState == PlayerStates.Running) {
            CurrentState = PlayerStates.Walking;
        }
    }
    private void SetTargetSpeed() {
        // Set the target speed based on the current state
        switch (CurrentState) {
            case PlayerStates.Idle:
            _targetSpeed = _idleSpeed;
            break;
            case PlayerStates.Walking:
            _targetSpeed = _walkSpeed;
            break;
            case PlayerStates.Running:
            _targetSpeed = _runSpeed;
            break;
        }
    }



    private void Update() {
        // Smoothly transition speed towards the target speed
        Speed = Mathf.Lerp(Speed, _targetSpeed, Time.deltaTime / _speedTransitionTime);

        Vector2 _inputVector = InputManager.Instance.GetInputVector().normalized;
        Vector3 _moveDirection = new Vector3(_inputVector.x, 0f, _inputVector.y);

        if (_moveDirection != Vector3.zero) {
            if (CurrentState != PlayerStates.Running) {
                CurrentState = PlayerStates.Walking;
            }
            _player.Move(_moveDirection * Speed * Time.deltaTime);
            OrientBody(_moveDirection);

        }
        else {
            CurrentState = PlayerStates.Idle;
        }


    }
    private void FixedUpdate() {
        // Hit a raycast in front of player to check for colliders

        float _maxDistance = 1f;
        Vector3 _offset = new Vector3(0f, 1f, 0f);

        if (Physics.Raycast(transform.position + _offset, transform.forward, out RaycastHit hit, _maxDistance, _collisionLayer)) {
            if (hit.collider.TryGetComponent<IInteractable>(out _interactable)) {
                _interactable.InteractStart();
                return;
            }
        }
        _interactable?.InteractEnd();
        _interactable = null;
    }

    private void OrientBody(Vector3 _moveDirection) {
        float _smoothTime = 0.1f;
        float _targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg;
        float _smoothDampAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref _currentVelocity, _smoothTime);
        transform.rotation = Quaternion.Euler(0f, _smoothDampAngle, 0f);
    }
}
