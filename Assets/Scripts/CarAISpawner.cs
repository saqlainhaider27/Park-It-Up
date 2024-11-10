using UnityEngine;

public class CarAISpawner : MonoBehaviour {

    private Transform _spawnPoint;
    
    private void Update() {
        
        Instantiate(_spawnPoint);

    }


}
