using UnityEngine;

public class CarAIController : Singleton<CarAIController> {

    [SerializeField] private DropPoint _dropPoint;
    public Vector3 DefaultDropLocation {
        get {
            return _dropPoint.transform.position;
        }
        private set {
            _dropPoint.transform.position = value;
        }
    }
    public void OccupyDropPoint() {
        _dropPoint.IsOccupied = true;
    }
    public void UnOccupyDropPoint() {
        _dropPoint.IsOccupied = false;
    }
    public bool GetIsDropPointOccupied() {
        return _dropPoint.IsOccupied;
    }
        
}

