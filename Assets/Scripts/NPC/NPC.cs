using System;
using UnityEngine;

public class NPC : MonoBehaviour {

    private BuildingPoints _reachedBuildingPoint = BuildingPoints.Entry;

    private NPCAI _NPCAI;

    public Car Car {
        get; set;
    }

    [field:SerializeField]
    public NPCSO NPCSO {
        get; private set;
    }
    public NPCStates CurrentState {
        get; set;
    }
    private void Awake() {
        _NPCAI = GetComponent<NPCAI>();
    }
    private void Start() {
        _NPCAI.OnNPCReached += NPCAI_OnNPCReached;
    }

    private void NPCAI_OnNPCReached(object sender, EventArgs e) {
        if (_reachedBuildingPoint == BuildingPoints.Exit) {
            return;
        }

        // Check if NPC reached entry or exitpoint
        Hide();
        // Show after sometime
        // Also change the transform to exit point
        Invoke(nameof(ShowAtExitPoint), 10f);


    }
    public void ShowAtExitPoint() {
        _reachedBuildingPoint = BuildingPoints.Exit;
        Show();
        transform.position = AIDestinationController.Instance.BuildingExitPoint;
        // Now make the player wait for the car
        Car.AskForCar();
        _NPCAI.SetDestination(AIDestinationController.Instance.WaitForCarPoint);
    }
    public void SitInCar() {
        Car.OccupyCar();
        Hide();
        // Move carAI to the finalDestination Location
        // Turn off the warning Particle in the car
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
    public void Show() {
        gameObject.SetActive(true);
    }

}
