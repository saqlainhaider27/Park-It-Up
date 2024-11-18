using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] private AudioRefsSO _audioRefsSO;
    private void Awake() {
        CarSpawner.Instance.OnCarSpawned += CarSpawner_OnCarSpawned;
        CarSpawner.Instance.OnCarDespawned += CarSpawner_OnCarDespawned;
        Player.Instance.OnPlayerMove += Player_OnPlayerMove;
    }

    private void Player_OnPlayerMove(object sender, Player.OnPlayerMoveEventArgs e) {
        while (Player.Instance.CurrentState != PlayerStates.Idle) {
            StartCoroutine(PlaySoundAfterDelay(e._position, e._duration));
        }
    }
    
    private void CarSpawner_OnCarDespawned(object sender, CarSpawner.OnCarDespawnedEventArgs e) {
        e._car.OnIdle -= _car_OnIdle;
        e._car.OnDrive -= _car_OnDrive;
        e._car.OnOff -= _car_OnOff;

        StopCarSound(e._car);
    }

    private void CarSpawner_OnCarSpawned(object sender, CarSpawner.OnCarSpawnedEventArgs e) {
        e._car.OnIdle += _car_OnIdle;
        e._car.OnDrive += _car_OnDrive;
        e._car.OnOff += _car_OnOff;
    }

    private void _car_OnOff(object sender, Car.OnOffEventArgs e) {
        StopCarSound(e._car);
    }

    private void _car_OnDrive(object sender, Car.OnDriveEventArgs e) {
        PlayCarLoopingSound(e._car, _audioRefsSO._carDrive);
    }

    private void _car_OnIdle(object sender, Car.OnIdleEventArgs e) {
        PlayCarLoopingSound(e._car, _audioRefsSO._carIdle);
    }

    private void PlayCarLoopingSound(Car car, AudioClip[] audioClips) {
        AudioSource audioSource = GetCarAudioSource(car);
        if (audioSource.isPlaying) {
            audioSource.Stop();
        }
        audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        audioSource.loop = true;
        audioSource.Play();
    }
    private IEnumerator PlaySoundAfterDelay(Vector3 _position , float _delay) {
        yield return new WaitForSeconds(_delay);
        PlaySound(_audioRefsSO._footSteps, _position);

    }


    private void StopCarSound(Car car) {
        AudioSource audioSource = GetCarAudioSource(car);
        if (audioSource.isPlaying) {
            audioSource.Stop();
        }
    }

    private AudioSource GetCarAudioSource(Car car) {
        AudioSource audioSource = car.GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = car.gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
        }
        return audioSource;
    }


    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
