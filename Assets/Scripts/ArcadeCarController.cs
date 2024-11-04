using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArcadeCarController : MonoBehaviour {

    private Rigidbody _rigidbody;

    [Header("References")]
    [SerializeField] private Transform[] _rayPoints;
    [SerializeField] private LayerMask _drivable;
    [SerializeField] private Transform _accelerationPoint;

    [Header("Suspension Settings")]
    [SerializeField] private float _springStiffness;
    [SerializeField] private float _damperStiffness;
    [SerializeField] private float _restLength;
    [SerializeField] private float _springTravel;
    [SerializeField] private float _wheelRadius;

    private float _moveInput = 0f;
    private float _steerInput = 0f;

    [Header("Car Settings")]
    [SerializeField] private float _acceleration = 25f;
    [SerializeField] private float _maxSpeed = 100f;
    [SerializeField] private float _deceleration = 10f;
    [SerializeField] private float _steerStrength = 15f;
    [SerializeField] private AnimationCurve _turningCurve;
    [SerializeField] private float _dragCoefficient = 1f;
    private Vector3 _currentCarLocalVelocity = Vector3.zero;
    private float _carVelocityRatio = 0f;

    private int[] _wheelIsGrounded = new int[4];
    private bool _isGrounded;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        GetPlayerInput();
    }
    private void FixedUpdate() {
        Suspension();
        GroundCheck();
        CalculateCarVelocity();
        Movement();
    }

    #region Movement

    private void Movement() {
        if (_isGrounded) {
            Acceleration();
            Deceleration();
            Turn();
            SideWaysDrag();
        }
    }
    private void Acceleration() {
        _rigidbody.AddForceAtPosition(_acceleration * _moveInput * transform.forward, _accelerationPoint.position, ForceMode.Acceleration);
    }
    private void Deceleration() {
        _rigidbody.AddForceAtPosition(_deceleration * _moveInput * -transform.forward, _accelerationPoint.position, ForceMode.Acceleration);
    }

    private void Turn() {
        _rigidbody.AddTorque(_steerStrength*_steerInput * _turningCurve.Evaluate(_carVelocityRatio) * Mathf.Sign(_carVelocityRatio) * transform.up, ForceMode.Acceleration);
    }
    private void SideWaysDrag() {
        float _currentSideWaysSpeed = _currentCarLocalVelocity.x;

        float _dragMagnitude = -_currentSideWaysSpeed * _dragCoefficient;

        Vector3 _dragForce = transform.right * _dragMagnitude;
        _rigidbody.AddForceAtPosition(_dragForce, _rigidbody.worldCenterOfMass, ForceMode.Acceleration);
    }

    #endregion

    #region Input Handling

    private void GetPlayerInput() {
        Vector2 _inputVector = InputManager.Instance.GetInputVector();
        _moveInput = _inputVector.y;
        _steerInput = _inputVector.x;
    }

    #endregion

    #region Car Status Check

    private void GroundCheck() {

        int _tempGroundedWheel = 0;

        for (int i = 0; i < _wheelIsGrounded.Length; i++) {
            _tempGroundedWheel += _wheelIsGrounded[i];
        }

        if (_tempGroundedWheel > 1) {
            // More than one wheel on the ground
            _isGrounded = true;
        }
        else {
            _isGrounded = false;
        }
    }

    private void CalculateCarVelocity() {
        _currentCarLocalVelocity = transform.InverseTransformDirection(_rigidbody.linearVelocity);
        _carVelocityRatio = _currentCarLocalVelocity.z / _maxSpeed;
    }
    
    #endregion

    #region Suspension Functions

    private void Suspension() {

        for (int i = 0; i < _rayPoints.Length; i++) {

            RaycastHit _hit;
            float _maxLength = _restLength + _springTravel;

            if (Physics.Raycast(_rayPoints[i].position, -_rayPoints[i].up, out _hit, _maxLength + _wheelRadius, _drivable)) {

                _wheelIsGrounded[i] = 1;

                float _currentSpringLength = _hit.distance - _wheelRadius;
                float _springCompression = _restLength - _currentSpringLength / _springTravel;

                float _springVelocity = Vector3.Dot(_rigidbody.GetPointVelocity(_rayPoints[i].position), _rayPoints[i].up);
                float _dampForce = _damperStiffness * _springVelocity;

                float _springForce = _springStiffness * _springCompression;

                float _netForce = _springForce - _dampForce;

                _rigidbody.AddForceAtPosition(_netForce * _rayPoints[i].up, _rayPoints[i].position);

                Debug.DrawLine(_rayPoints[i].position, _hit.point, Color.red);
            }
            else {
                _wheelIsGrounded[i] = 0;
                Debug.DrawLine(_rayPoints[i].position, _rayPoints[i].position + (_wheelRadius + _maxLength) * -_rayPoints[i].up);
            }
        }

    } 
    #endregion
}