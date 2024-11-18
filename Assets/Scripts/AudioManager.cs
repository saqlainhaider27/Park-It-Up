using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    [SerializeField] private AudioRefsSO _audioClipsRefsSO;
    private void Awake() {
        CarSpawner.Instance.OnCarSpawned += CarSpawner_OnCarSpawned;
        CarSpawner.Instance.OnCarDespawned += CarSpawner_OnCarDespawned;
        Player.Instance.OnPlayerMove += Player_OnPlayerMove;

    }
    private void Player_OnPlayerMove(object sender, Player.OnPlayerMoveEventArgs e) {
        PlaySound(_audioClipsRefsSO._footSteps, e._position);
    }

    private void CarSpawner_OnCarDespawned(object sender, CarSpawner.OnCarDespawnedEventArgs e) {
        e._car.OnIdle -= Car_OnIdle;
        e._car.OnDrive -= Car_OnDrive;
        e._car.OnOff -= Car_OnOff;

        StopCarSound(e._car);
    }

    private void CarSpawner_OnCarSpawned(object sender, CarSpawner.OnCarSpawnedEventArgs e) {
        e._car.OnIdle += Car_OnIdle;
        e._car.OnDrive += Car_OnDrive;
        e._car.OnOff += Car_OnOff;
    }

    private void Car_OnOff(object sender, Car.OnOffEventArgs e) {
        StopCarSound(e._car);
    }

    private void Car_OnDrive(object sender, Car.OnDriveEventArgs e) {
        PlayCarLoopingSound(e._car, _audioClipsRefsSO._carDrive);
    }

    private void Car_OnIdle(object sender, Car.OnIdleEventArgs e) {
        PlayCarLoopingSound(e._car, _audioClipsRefsSO._carIdle);
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
