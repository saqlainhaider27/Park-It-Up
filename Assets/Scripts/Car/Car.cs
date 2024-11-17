using UnityEngine;

public class Car : MonoBehaviour, IInteractable {

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
    [SerializeField] public ParticleSystem _carWarning;
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
            // Delete Car
            DestroySelf();
            // Delete NPC associated with car
            Destroy(_generatedNPC.gameObject);
            // Make the spawned cars less to enable more to spawn
            CarSpawner.Instance.DecrementSpawnedCars();
        }

    }

    private void Player_OnInteractionExit(object sender, Player.OnInteractionEventArgs e) {
        if (e._interactionObject == (IInteractable)this) {
            DisableDriving();
            Player.Instance.Show();
            Player.Instance.SetTransformPosition(ExitPoint.position);
        }
    }

    private void Player_OnInteractionEnter(object sender, Player.OnInteractionEventArgs e) {
        if (!Drivable) {
            return;
        }
        if (e._interactionObject == (IInteractable)this) {
            Interact();
            Player.Instance.Hide();
            Player.Instance.SetTransformPosition(ExitPoint.position);
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
