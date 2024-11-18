using System;
using UnityEngine;

public class ParkingPoint : Point {

    public event EventHandler OnOccupuied;
    [SerializeField] private GameObject _occupied;
    [SerializeField] private GameObject _upOccupied;

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
            _car.CurrentState = CarStates.Off;
            _occupied.SetActive(true);
            _upOccupied.SetActive(false);

            OnOccupuied?.Invoke(this, EventArgs.Empty);
            if (this is PickupPoint) {
                return;
            }
            ParkingManager.Instance.AddParkingPointToOccupyList(this);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.TryGetComponent<Car>(out Car _car)) {
            Occupied = false;
            _upOccupied.SetActive(true);
            _occupied.SetActive(false);
            ParkingManager.Instance.RemoveParkingPointFromOccupyList(this);
            _occupiedByCar = null;
        }
    }
}