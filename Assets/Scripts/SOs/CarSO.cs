using UnityEngine;

[CreateAssetMenu(fileName = "CarSO", menuName = "Scriptable Objects/CarAISO")]
public class CarSO : ScriptableObject {

    public int carId;
    public GameObject carPrefab;
}
