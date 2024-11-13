using UnityEngine;

public class Car : MonoBehaviour, IInteractable {

    private CarAI _carAI;

    private PrometeoCarController _carController;

    [field: SerializeField]
    public Transform ExitPoint {
        get; private set;
    }
    public bool Occupied {
        get; private set;
    }
    [SerializeField] private SelectedCarBody _selectedCarBody;

    [field: SerializeField]
    public CarSO CarSO {
        get; private set;
    }

    private void Awake() {
        _carController = GetComponent<PrometeoCarController>();
        _carAI = GetComponent<CarAI>();

        DisableDriving();
    }

    private void Start() {

        Player.Instance.OnInteractionEnter += Player_OnInteractionEnter;
        Player.Instance.OnInteractionExit += Player_OnInteractionExit;

        _carAI.OnCarReached += CarAI_OnCarReached;
    }

    private void CarAI_OnCarReached(object sender, System.EventArgs e) {
        NPC _generatedNPC = NPCSpawner.Instance.SpawnNPC(ExitPoint.position);
    }

    private void Player_OnInteractionExit(object sender, Player.OnInteractionEventArgs e) {
        if (e._interactionObject == (IInteractable)this) {
            DisableDriving();
        }
    }

    private void Player_OnInteractionEnter(object sender, Player.OnInteractionEventArgs e) {
        if (e._interactionObject == (IInteractable)this) {
            Interact();
        }
    }


    public void EnableDriving() {
        Occupied = true;
        _carAI.enabled = false;
        _carController.enabled = true;
    }
    public void DisableDriving() {
        _carAI.enabled = true;
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