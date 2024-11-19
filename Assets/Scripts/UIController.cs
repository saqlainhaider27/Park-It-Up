using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController> {
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Menu _pauseMenu;
    [SerializeField] private Menu _UIBlur;

    private void Start() {

        ScoreController.Instance.OnScoreChanged += ScoreController_OnScoreChanged;
        InputManager.Instance.OnEscapeKeyPressed += InputActions_OnEscapeKeyPressed;
    }

    private void InputActions_OnEscapeKeyPressed(object sender, EventArgs e) {
        TogglePause();
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

    private void ScoreController_OnScoreChanged(object sender, EventArgs e) {
        _scoreText.text = ScoreController.Instance.Score.ToString();
    }
}
