using UnityEngine;

public class CarAIController : Singleton<CarAIController> {

    [SerializeField] private Transform _defaultDropLocation;
    public Vector3 DefaultDropLocation {
        get {
            return _defaultDropLocation.position;
        }
        private set {
            _defaultDropLocation.position = value;
        }
    }
}

