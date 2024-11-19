using System;
using System.Collections;
using UnityEngine;

public class Menu : MonoBehaviour {

    public event EventHandler OnShow;
    public event EventHandler OnHide;

    public void Show() {
        gameObject.SetActive(true);
        OnShow?.Invoke(this, EventArgs.Empty);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }
    public IEnumerator HideAfterDelay(float _delay) {
        yield return new WaitForSecondsRealtime(_delay);
        Hide();
    }
    public void InvokeOnHide() {
        OnHide?.Invoke(this, EventArgs.Empty);
    }
}