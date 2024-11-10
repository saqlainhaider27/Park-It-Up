using UnityEngine;

public class DropPoint : MonoBehaviour {

    private bool _isOccupied;
    public bool IsOccupied {
        get {
            return _isOccupied;
        }
        set {
            _isOccupied = value;
        }
    }


}
