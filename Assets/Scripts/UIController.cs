using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController> {

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Menu _pauseMenu;
    [SerializeField] private Menu _UIBlur;

    [SerializeField] private Button _pauseButton;

    public event EventHandler OnHomeButtonPressed;

    private void Start() {

        ScoreController.Instance.OnScoreChanged += ScoreController_OnScoreChanged;
        InputManager.Instance.OnEscapeKeyPressed += InputActions_OnEscapeKeyPressed;
    }

    private void InputActions_OnEscapeKeyPressed(object sender, EventArgs e) {
        _pauseButton.onClick.Invoke();        
    }

    public void TogglePause() {
        if (GameManager.Instance.CurrentState != GameStates.Paused) {
            Pause();
        }
        else {
            Resume();
        }
    }
    public void Pause() {
        
        GameManager.Instance.CurrentState = GameStates.Paused;
        _UIBlur.Show();
        _pauseMenu.Show();
        Time.timeScale = 0f;
    }
    public void Resume() {
        Time.timeScale = 1f;
        GameManager.Instance.CurrentState = GameStates.Play;

        _UIBlur.InvokeOnHide();
        _pauseMenu.InvokeOnHide();

        _UIBlur.HideAfterDelay(1f);
        _pauseMenu.HideAfterDelay(1f);

    }
    public void Home() {
        Time.timeScale = 1f;
        OnHomeButtonPressed?.Invoke(this, EventArgs.Empty);
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
    private void ScoreController_OnScoreChanged(object sender, EventArgs e) {
        _scoreText.text = ScoreController.Instance.Score.ToString();
    }

}
