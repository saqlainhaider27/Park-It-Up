using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController> {

    public event EventHandler OnSceneChanged;

    private void Start() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            MenuController.Instance.OnPlayButtonPressed += MenuController_OnPlayButtonPressed;
            return;
        }

        UIController.Instance.OnHomeButtonPressed += UIController_OnHomeButtonPressed;
    }


    private void MenuController_OnPlayButtonPressed(object sender, EventArgs e) {
        LoadNextScene();
    }

    private void UIController_OnHomeButtonPressed(object sender, EventArgs e) {
        LoadMainMenuScene();
    }

    public void LoadMainMenuScene() {
        int loadSceneIndex = 0;
        StartCoroutine(LoadSceneWithTransition(loadSceneIndex));
    }
    private void LoadNextScene() {
        int loadSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadSceneWithTransition(loadSceneIndex));
    }


    private IEnumerator LoadSceneWithTransition(int index) {
        OnSceneChanged?.Invoke(this, new EventArgs());
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
}
