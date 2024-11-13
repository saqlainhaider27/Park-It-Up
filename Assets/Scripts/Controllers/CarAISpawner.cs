using System.Collections.Generic;
using UnityEngine;

public class CarAISpawner : MonoBehaviour {

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Car> _carList = new List<Car>();
    [SerializeField] private float maxSpawnDuration;
    private float spawnCooldown;

    private void Start() {
        // Initialize the spawnCooldown to start spawning immediately.
        spawnCooldown = 0f;
    }

    private void Update() {
        // Reduce the cooldown over time
        spawnCooldown -= Time.deltaTime;

        // If cooldown reaches 0, spawn a new car and reset the cooldown
        // Don't Spawn any cars if drop point occupued
        if (CarAIController.Instance.GetIsDropPointOccupied()) {
            return;
        }
        if (spawnCooldown <= 0f) {
            Car _generatedCar = Utils.SpawnRandomFromList<Car>(_spawnPoint.position, _carList);
            // Reset the cooldown timer
            spawnCooldown = maxSpawnDuration;
        }
    }
}
