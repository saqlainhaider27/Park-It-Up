using UnityEngine;

public class Car : MonoBehaviour, IInteractable {

    private CarAI _carAI;

    private PrometeoCarController _carController;
    [SerializeField] private Transform _exitPoint;
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
        InputManager.Instance.OnInteractKeyPressed += InputManager_OnInteractKeyPressed;
    }
    private void InputManager_OnInteractKeyPressed(object sender, System.EventArgs e) {
        // Check is player state is driving?
        // Enable player as player trying to exit state
        // Call a function to change the transform of the player to the exit transform
        // Disable the CarController and enable AI
        if (Player.Instance.CurrentState != PlayerStates.Driving) {
            return;
        }
        Player.Instance.Show();
        DisableDriving();
        Player.Instance.SetTransformPosition(_exitPoint);
    }


    public void EnableDriving() {
        _carAI.enabled = false;
        _carController.enabled = true;
    }
    public void DisableDriving() {
        _carAI.enabled = true;
        _carController.enabled = false;
    }

    public void Interact() {
        // Hide Player and change to driving
        Player.Instance.Hide();
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