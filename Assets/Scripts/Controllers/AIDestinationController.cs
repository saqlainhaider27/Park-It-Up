using UnityEngine;

public class AIDestinationController : Singleton<AIDestinationController> {

    [SerializeField] private DropPoint _dropPoint;
    [SerializeField] private GameObject _entryPoint;
    [SerializeField] private GameObject _exitPoint;
    public Vector3 DefaultDropLocation {
        get {
            return _dropPoint.transform.position;
        }
        private set {
            _dropPoint.transform.position = value;
        }
    }
    public Vector3 BuildingEntryPoint {
        get {
            return _entryPoint.transform.position;
        }
        private set {
            _entryPoint.transform.position = value;
        }

    }
    public Vector3 BuildingExitPoint{
        get {
            return _exitPoint.transform.position;
        }
        private set {
            _exitPoint.transform.position = value;
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

