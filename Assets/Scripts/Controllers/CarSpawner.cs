using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Car> _carList = new List<Car>();
    private int _spawnedCars = 0;
    [SerializeField] private int _maxSpawnedCars;


    private void Awake() {
        AIDestinationController.Instance.OnDropPointUnOccupuied += AIDestinationController_OnDropPointUnOccupuied;
    }
    // TODO: Spawn new car when player parks the previous car
    private void AIDestinationController_OnDropPointUnOccupuied(object sender, System.EventArgs e) {
        if (_spawnedCars > _maxSpawnedCars) {
            return;
        }
        _spawnedCars++;
        Car _generatedCar = Utils.SpawnRandomFromList<Car>(_spawnPoint.position, _carList);
    }
}
