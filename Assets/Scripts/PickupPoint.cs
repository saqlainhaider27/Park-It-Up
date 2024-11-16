using System.Collections.Generic;
using UnityEngine;

public class PickupPoint : ParkingPoint{


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
                _occupiedByCar = null;
                break;
            }
        }
    }
}
