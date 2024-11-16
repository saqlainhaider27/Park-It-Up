using System;
using UnityEngine;

public class ParkingSpot : MonoBehaviour {

    public event EventHandler OnOccupuied;
    protected Car _occupiedByCar;
    public bool Occupied {
        get; private set;
    }
    private void Awake() {
        Occupied = false;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.TryGetComponent<Car>(out Car _car)) {
            if (Occupied) {
                return;
            }
            if (_car.IsPlayerDriving) {
                return;
            }
            _car.transform.position = transform.position;
            _car.transform.rotation = transform.rotation;
            _occupiedByCar = _car;
            Occupied = true;
            OnOccupuied?.Invoke(this, EventArgs.Empty);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.TryGetComponent<Car>(out Car _car)) {
            Occupied = false;
            _occupiedByCar = null;
        }
    }
}