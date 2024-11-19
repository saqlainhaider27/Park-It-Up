using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public GameStates CurrentState {
        get; set;
    }
}