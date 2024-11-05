using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour {

    [Header("Car Settings")]
    [field: SerializeField]
    public float MaxSpeed {
        get; private set;
    }
    [field: SerializeField]
    public float Acceleration {
        get; private set;
    }
    [field: SerializeField]
    public float Deceleration {
        get; private set;
    }

    [System.Serializable]
    public class Wheel {
        public Transform _tireTransform;
        public WheelCollider _wheelCollider;
        public Axel _axel;
    }
    [SerializeField] private List<Wheel> _carWheels = new List<Wheel>();

    public enum Axel {
        Front,
        Rear
    }

    [Header("Suspension Settings")]
    [SerializeField] private float _springUnstrechLength;
    [SerializeField] private float _suspensionRestDistance;

    [Header("References")]
    [SerializeField] private LayerMask _drivable;
    private Rigidbody _carRB;

    private void Start() {
        _carRB = GetComponent<Rigidbody>();
    }
    private void Update() {
        
    }

    private void FixedUpdate() {
        // Suspension function in FixedUpdate as it hits a raycast from each wheel
        Suspension();
    }
    private void Suspension() {
        foreach (Wheel wheel in _carWheels) {
            // Perform raycast from wheel center
            Ray _wheelRay = new Ray(wheel._tireTransform.position, -transform.up);
            bool rayDidHit = Physics.Raycast(_wheelRay, _springUnstrechLength, _drivable);
            if (rayDidHit) {
                Vector3 _springDir = wheel._tireTransform.up;

                Vector3 _wheelWorldVelocity = _carRB.GetPointVelocity(wheel._tireTransform.position);

                float velocity = Vector3.Dot(_springDir, _wheelWorldVelocity);
            }
        }


    }


    private void Move(Vector3 _direction) {

    }



}