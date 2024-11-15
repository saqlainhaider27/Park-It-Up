using UnityEngine;

public class ParkingSpot : MonoBehaviour {
    
    public bool Occupied {
        get; private set;
    }
    private void Awake() {
        Occupied = false;
    }
    private void OnTriggerEnter(Collider other) {
        if (Occupied) {
            return;
        }
        if (other.gameObject.TryGetComponent<Car>(out Car _car)) {
            Occupied = true;
            // Also set the transform of the car to this transform when player gets out of the car
            _car.transform.position = this.transform.position;
        }

    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.TryGetComponent<Car>(out Car _car)) {
            Occupied = false;
        }
    }
}