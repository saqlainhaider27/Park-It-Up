using System;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : Singleton<CarSpawner> {

    public event EventHandler<OnCarSpawnedEventArgs> OnCarSpawned;
    public event EventHandler<OnCarDespawnedEventArgs> OnCarDespawned;
    public class OnCarDespawnedEventArgs : EventArgs {
        public Car _car;
    }
    public class OnCarSpawnedEventArgs : EventArgs { 
        public Car _car;
    }
    
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Car> _carList = new List<Car>(); 
    private int _spawnedCars = 0;
    [SerializeField] private int _maxSpawnedCars = 5;

    private bool _isSpawned = false;
   
    private void Start() {

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
        _spawnedCars++;
        Car _generatedCar = Utils.SpawnRandomFromList<Car>(_spawnPoint.position, _carList);
        Debug.Log("Spawned");
        OnCarSpawned?.Invoke(this, new OnCarSpawnedEventArgs {
            _car = _generatedCar
        });
        Invoke(nameof(ResetIsSpawned), _resetCooldown);
    }
    public void ResetIsSpawned() {
        _isSpawned = false;
    }

    public void DecrementSpawnedCars(Car _despawnedCar) {
        OnCarDespawned?.Invoke(this, new OnCarDespawnedEventArgs {
            _car = _despawnedCar
        });
        _spawnedCars--;
        if (_spawnedCars <=  0 ) {
            SpawnCar();
        }
    }
}
