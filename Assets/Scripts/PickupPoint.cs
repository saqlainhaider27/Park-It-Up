using System.Collections.Generic;
using UnityEngine;

public class PickupPoint : ParkingSpot{


    private void Start() {
        OnOccupuied += PickupPoint_OnOccupuied;
    }

    private void PickupPoint_OnOccupuied(object sender, System.EventArgs e) {
        Debug.Log("Occupied");
        // Check if it is occupied by the required car
        // Required car is the car that npc currently active at the pickup point requires
        List<NPC> _toServeNPC = new List<NPC>();
        _toServeNPC = NPCSpawner.Instance.GetSpawnedNPCs();
        if (_toServeNPC.Count <= 0) {
            // There are no npc
            Debug.Log("NoNPCs");
            return;
        }
        // There are npc serve the top npc in the list
        if (_toServeNPC[0].Car == _occupiedByCar) {
            // The PickupPoint is occupied by the correct Car
            _toServeNPC[0].SitInCar();
            _toServeNPC.RemoveAt(0);
            _occupiedByCar = null;
        }
    }
}
