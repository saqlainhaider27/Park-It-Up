using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController> {
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake() {
        ScoreController.Instance.OnScoreChanged += ScoreController_OnScoreChanged;
    }

    private void ScoreController_OnScoreChanged(object sender, EventArgs e) {
        _scoreText.text = ScoreController.Instance.Score.ToString();
    }
}
