using System;
using UnityEngine;

public class Car : MonoBehaviour, IInteractable {
    public event EventHandler<OnIdleEventArgs> OnIdle;
    public event EventHandler<OnDriveEventArgs> OnDrive;
    public event EventHandler<OnOffEventArgs> OnOff;
    public class OnIdleEventArgs : EventArgs {
        public Car _car;
    }
    public class OnDriveEventArgs : EventArgs {
        public Car _car;
    }
    public class OnOffEventArgs : EventArgs {
        public Car _car;
    }
    public CarStates _currentState;
    public CarStates CurrentState {
        get {
            return _currentState;
        }
        set {
            _currentState = value;
            switch (CurrentState) {
                case CarStates.Idle:
                OnIdle?.Invoke(this, new OnIdleEventArgs {
                        _car = this
                });
                break;
                case CarStates.Drive:
                OnDrive?.Invoke(this, new OnDriveEventArgs {
                    _car = this
                });
                break;
                case CarStates.Off:
                OnOff?.Invoke(this, new OnOffEventArgs{
                    _car = this
                });
                break;
            }
        }
    }

    private CarReachPoints _reachPoint;

    private NPC _generatedNPC;
    private CarAI _carAI;
    public bool IsPlayerDriving {
        get; private set;
    }
    public bool Drivable {
        get; private set;
    }
    private CarController _carController;

    [field: SerializeField]
    public Transform ExitPoint {
        get; private set;
    }
    [SerializeField] private SelectedCarBody _selectedCarBody;

    [field: SerializeField]
    public CarSO CarSO {
        get; private set;
    }
    [SerializeField] private ParticleSystem _carWarning;
    private bool npcSpawned = false;

    private void Awake() {
        _carController = GetComponent<CarController>();
        _carAI = GetComponent<CarAI>();
        Drivable = false;
        _carAI.enabled = true;
        DisableDriving();
    }
      
    private void Start() {
        Player.Instance.OnInteractionEnter += Player_OnInteractionEnter;
        Player.Instance.OnInteractionExit += Player_OnInteractionExit;
        _reachPoint = CarReachPoints.Drop;
        _carAI.OnCarReached += CarAI_OnCarReached;
    }

    private void CarAI_OnCarReached(object sender, System.EventArgs e) {
        CurrentState = CarStates.Idle;
        if (_reachPoint == CarReachPoints.Drop) {
            if (!npcSpawned) {
                Drivable = true;
                npcSpawned = true;
                AIDestinationController.Instance.OccupyDropPoint();
                _generatedNPC = NPCsManager.Instance.SpawnNPC(ExitPoint.position);
                _generatedNPC.Car = this;
            }
        }
        else {
            // Make the spawned cars less to enable more to spawn
            CarSpawner.Instance.DecrementSpawnedCars(this);
            // Delete Car
            DestroySelf();
            // Delete NPC associated with car
            Destroy(_generatedNPC.gameObject);


        }

    }

    private void Player_OnInteractionExit(object sender, Player.OnInteractionEventArgs e) {
        if (e._interactionObject == (IInteractable)this) {
            DisableDriving();
            Player.Instance.Show();
            Player.Instance.transform.position = ExitPoint.position;
        }
    }

    private void Player_OnInteractionEnter(object sender, Player.OnInteractionEventArgs e) {
        if (!Drivable) {
            return;
        }
        if (e._interactionObject == (IInteractable)this) {
            Interact();
            Player.Instance.Hide();
            
            // Also unoccupy the DropPoint
            AIDestinationController.Instance.UnOccupyDropPoint();
        }
    }

    public void EnableDriving() {
        IsPlayerDriving = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        _carAI.enabled = false;
        _carController.enabled = true;
    }
    public void DisableDriving() {
        // Break the car;
        IsPlayerDriving = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _carController.enabled = false;

    }
    public void GetBackInCar() {
        Drivable = false;
        _carWarning.Stop();
        _reachPoint = CarReachPoints.Final;
        _carAI.enabled = true;
        _carAI.UpdateDestination(AIDestinationController.Instance.FinalDestination);

    }
    public void StopAskingForCar() {
        _carWarning.Stop();
    }
    public void AskForCar() {
        _carWarning.Play();
    }

    public void Interact() {
        EnableDriving();
        InteractEnd();
    }

    public void InteractStart() {
        _selectedCarBody.Show();
    }

    public void InteractEnd() {
        _selectedCarBody.Hide();
    }
    public void DestroySelf() {
        Destroy(this.gameObject);
    }
}
