using System;
using UnityEngine;

public class NPC : MonoBehaviour {

    private BuildingPoints _reachedBuildingPoint = BuildingPoints.Entry;

    public event EventHandler<OnNPCWalkingEventArgs> OnNPCWalking;
    public event EventHandler<OnNPCIdleEventArgs> OnNPCIdle;
    public class OnNPCIdleEventArgs : EventArgs {
        public NPC _NPC;
    }
    public class OnNPCWalkingEventArgs : EventArgs {
        public NPC _NPC;
    }


    private NPCAI _NPCAI;

    public Car Car {
        get; set;
    }

    [field:SerializeField]
    public NPCSO NPCSO {
        get; private set;
    }
    private NPCStates _currentState;
    public NPCStates CurrentState {
        get {
            return _currentState;
        }
        set {
            _currentState = value;
            switch (CurrentState) {
                case NPCStates.Idle:
                OnNPCIdle?.Invoke(this, new OnNPCIdleEventArgs {
                    _NPC = this
                });
                break;
                case NPCStates.Walking:
                OnNPCWalking?.Invoke(this, new OnNPCWalkingEventArgs {
                    _NPC = this
                });
                break;
            }
        }
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
        NPCsManager.Instance.AddNPCToWaitingForCarList(this);
        _NPCAI.SetDestination(AIDestinationController.Instance.WaitForCarPoint);
        
    }
    public void SitInCar() {
        Car.GetBackInCar();
        Hide();
        NPCsManager.Instance.RemoveNPCFromWaitingForCarList(this);
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
