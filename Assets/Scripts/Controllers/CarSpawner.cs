using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Car> _carList = new List<Car>();
    private float spawnCooldown;

    private void Awake() {
        AIDestinationController.Instance.OnDropPointUnOccupuied += AIDestinationController_OnDropPointUnOccupuied;
    }
    private void AIDestinationController_OnDropPointUnOccupuied(object sender, System.EventArgs e) {
        Car _generatedCar = Utils.SpawnRandomFromList<Car>(_spawnPoint.position, _carList);
    }
}
