using UnityEngine;

public class Car : MonoBehaviour, IInteractable {

    private CarAI _carAI;
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

        _carAI.OnCarReached += CarAI_OnCarReached;
    }

    private void CarAI_OnCarReached(object sender, System.EventArgs e) {
        if (!npcSpawned) {
            Drivable = true;
            npcSpawned = true;

            NPC _generatedNPC = NPCSpawner.Instance.SpawnNPC(ExitPoint.position);
            _generatedNPC.Car = this;
        }
    }

    private void Player_OnInteractionExit(object sender, Player.OnInteractionEventArgs e) {
        if (e._interactionObject == (IInteractable)this) {
            DisableDriving();
            Player.Instance.Show();
            Player.Instance.SetTransformPosition(ExitPoint.position);

            // Set the position to the parking spot if car exit on parking spot
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
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        _carAI.enabled = false;
        _carController.enabled = true;
    }
    public void DisableDriving() {
        // Break the car;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _carController.enabled = false;

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

}
