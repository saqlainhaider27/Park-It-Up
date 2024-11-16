using System;
using UnityEngine;

public class ParkingPoint : Point {

    public event EventHandler OnOccupuied;
    protected Car _occupiedByCar;
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
            ParkingManager.Instance.AddParkingPointToOccupyList(this);
            OnOccupuied?.Invoke(this, EventArgs.Empty);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.TryGetComponent<Car>(out Car _car)) {
            Occupied = false;
            ParkingManager.Instance.RemoveParkingPointFromOccupyList(this);
            _occupiedByCar = null;
        }
    }
}