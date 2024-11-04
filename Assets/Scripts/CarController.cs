using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour, ICar {
    #region Fields

    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration; 

    private Rigidbody _rigidbody;

    #endregion
    #region Properties

    public float Speed {
        get {
            return _speed;
        }
        private set {
            _speed = value;
        }
    }

    public float Acceleration {
        get {
            return _acceleration;
        }
        private set {
            _acceleration = value;
        }
    }

    #endregion

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void Update() {
        Vector2 _inputVector = InputManager.Instance.GetInputVector();
        Vector3 _moveDirection = new Vector3(_inputVector.x, 0f, _inputVector.y);
        if (_moveDirection == Vector3.zero) {
            return;
        }
        Move(_moveDirection);
    }

    public void Move(Vector3 _direction) {
        if (_direction.z > 0f) {
            // Moving forward
            _rigidbody.AddForce(_direction * Speed * Time.deltaTime);
        }
        else if (_direction.z < 0f) {
            // Reverse
            _rigidbody.AddForce(_direction * Speed * Time.deltaTime);

        }
        else if (_direction.x > 0f) {
            // Turning right
        }
        else if (_direction.x < 0f) {
            // Turning left
        }
        
    }
}
