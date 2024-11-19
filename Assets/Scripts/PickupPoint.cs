using System;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoint : ParkingPoint{

    public static PickupPoint _instance;
    public static PickupPoint Instance { get { return _instance; } }

    public event EventHandler OnCorrectCar;

    private void Awake() {
        if (_instance != this && _instance != null) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }
    private void Start() {
        OnOccupuied += PickupPoint_OnOccupuied;
    }

    private void PickupPoint_OnOccupuied(object sender, System.EventArgs e) {
        // Check if it is occupied by the required car
        // Required car is the car that npc currently active at the pickup point requires
        List<NPC> _waitingForCarList = new List<NPC>();
        _waitingForCarList = NPCsManager.Instance.GetNPCsWaitingForCarList();
        if (_waitingForCarList.Count <= 0) {
            // There are no npc
            return;
        }
        // There are npc serve the top npc in the list
        foreach (NPC _NPC in _waitingForCarList) {
            if (_NPC.Car == _occupiedByCar) {
                _NPC.SitInCar();
                OnCorrectCar?.Invoke(this, EventArgs.Empty);
                _occupiedByCar = null;
                break;
            }
        }
    }
}
