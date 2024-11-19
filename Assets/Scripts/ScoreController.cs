using System;
using UnityEngine;

public class ScoreController : Singleton<ScoreController> {

    public event EventHandler OnScoreChanged;

    public int Score {
        get; private set;
    }
    private void Start() {
        PickupPoint.Instance.OnCorrectCar += PickupPoint_OnCorrectCar;
    }

    private void PickupPoint_OnCorrectCar(object sender, EventArgs e) {
        Score++;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }
}