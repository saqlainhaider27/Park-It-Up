using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : Singleton<CarSpawner> {

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Car> _carList = new List<Car>(); 
    private int _spawnedCars = 0;
    [SerializeField] private int _maxSpawnedCars = 5;

    private bool _isSpawned = false;
    

    private void Awake() {
        
        // Spawn the initial car to make the loop going
        SpawnCar();
        ParkingManager.Instance.OnParkingPointOccupied += ParkingManager_OnParkingPointOccupied;
    }

    private void ParkingManager_OnParkingPointOccupied(object sender, System.EventArgs e) {
        if (_isSpawned) {
            return;
        }
        SpawnCar();
    }

    private void SpawnCar() {
        if (_spawnedCars > _maxSpawnedCars) {
            return;
        }
        _isSpawned = true;
        float _resetCooldown = 1f;
        Invoke(nameof(ResetIsSpawned), _resetCooldown);
        

        _spawnedCars++;
        Car _generatedCar = Utils.SpawnRandomFromList<Car>(_spawnPoint.position, _carList);
    }
    public void ResetIsSpawned() {
        _isSpawned = false;
    }

    public void DecrementSpawnedCars() {
        _spawnedCars--;
        if (_spawnedCars <=  0 ) {
            SpawnCar();
        }
    }
}
