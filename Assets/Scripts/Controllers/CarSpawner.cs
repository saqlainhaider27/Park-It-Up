using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : Singleton<CarSpawner> {

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Car> _carList = new List<Car>();
    private int _spawnedCars = 0;
    [SerializeField] private int _maxSpawnedCars = 5;


    private void Awake() {
        
        // Spawn the initial car to make the loop going
        SpawnCar();
        ParkingManager.Instance.OnParkingPointOccupied += ParkingManager_OnParkingPointOccupied;

        // AIDestinationController.Instance.OnDropPointUnOccupuied += AIDestinationController_OnDropPointUnOccupuied;
    }

    private void ParkingManager_OnParkingPointOccupied(object sender, System.EventArgs e) {
        SpawnCar();
    }

    // TODO: Spawn new car when player parks the previous car
    private void AIDestinationController_OnDropPointUnOccupuied(object sender, System.EventArgs e) {
        // SpawnCar();
    }

    private void SpawnCar() {
        if (_spawnedCars > _maxSpawnedCars) {
            return;
        }
        _spawnedCars++;
        Car _generatedCar = Utils.SpawnRandomFromList<Car>(_spawnPoint.position, _carList);
    }
    public void DecrementSpawnedCars() {
        _spawnedCars--;
    }
}
