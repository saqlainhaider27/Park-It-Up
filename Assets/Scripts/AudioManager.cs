using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] private AudioRefsSO _audioRefsSO;
    private void Awake() {
        CarSpawner.Instance.OnCarSpawned += CarSpawner_OnCarSpawned;
        CarSpawner.Instance.OnCarDespawned += CarSpawner_OnCarDespawned;
        
    }

    private void CarSpawner_OnCarDespawned(object sender, CarSpawner.OnCarDespawnedEventArgs e) {
        e._car.OnIdle -= _car_OnIdle;
        e._car.OnDrive -= _car_OnDrive;
        e._car.OnOff -= _car_OnOff;
    }

    private void CarSpawner_OnCarSpawned(object sender, CarSpawner.OnCarSpawnedEventArgs e) {
        
        e._car.OnIdle += _car_OnIdle;
        e._car.OnDrive += _car_OnDrive;
        e._car.OnOff += _car_OnOff;
    }

    private void _car_OnOff(object sender, Car.OnOffEventArgs e) {
        //Stop all the playing sounds of this car
        
    }

    private void _car_OnDrive(object sender, Car.OnDriveEventArgs e) {
        PlaySound(_audioRefsSO._carIdle, e._car.transform.position);
    }

    private void _car_OnIdle(object sender, Car.OnIdleEventArgs e) {
        PlaySound(_audioRefsSO._carDrive, e._car.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

}
