using System;
using System.Collections;
using UnityEngine;

public class MenuController : Singleton<MenuController> {
    [SerializeField] private AudioSource _musicSource;
    private float _transitionDuration = 1f;

    public event EventHandler OnPlayButtonPressed;
    private void Start() {
        _musicSource.volume = 0f;
        StartCoroutine(MusicFadeIn());
    }
    public void Play() {
        OnPlayButtonPressed?.Invoke(this, EventArgs.Empty);
        StartCoroutine(MusicFadeOut());
    }

    public void Quit() {
#if UNITY_EDITOR
        // Stop playing the scene in the editor.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the application.
        Application.Quit();
#endif
    }
    public IEnumerator MusicFadeIn() {
        float targetVolume = 0.1f; // Define the target volume
        float startVolume = 0f; // Start from zero
        _musicSource.volume = startVolume; // Ensure starting volume is zero
        while (_musicSource.volume < targetVolume) {
            _musicSource.volume += targetVolume * Time.deltaTime / _transitionDuration;
            yield return null;
        }

        _musicSource.volume = targetVolume; // Ensure volume is set to the target at the end
    }

    public IEnumerator MusicFadeOut() {
        float startVolume = _musicSource.volume;

        while (_musicSource.volume > 0) {
            _musicSource.volume -= startVolume * Time.deltaTime / _transitionDuration;
            yield return null;
        }

        _musicSource.volume = 0; // Ensure volume is zero at the end
    }

}