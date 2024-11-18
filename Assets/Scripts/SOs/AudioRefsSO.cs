using UnityEngine;

[CreateAssetMenu(fileName = "AudioRefs", menuName = "Scriptable Objects/AudioRefs")]
public class AudioRefsSO : ScriptableObject {

    public AudioClip[] _carIdle;
    public AudioClip[] _carDrive;
}
