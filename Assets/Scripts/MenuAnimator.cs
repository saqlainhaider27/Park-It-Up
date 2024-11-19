using UnityEngine;

public class MenuAnimator : MonoBehaviour {
    private const string FADE_IN = "FadeIn";
    private const string FADE_OUT = "FadeOut";
    private Menu _menu;
    private Animator _animator;
    private void Awake() {
        _animator = GetComponent<Animator>();
        _menu = GetComponent<Menu>();
    }
    private void Start() {
        _menu.OnShow += Menu_OnShow;
        _menu.OnHide += Menu_OnHide;
    }

    private void Menu_OnShow(object sender, System.EventArgs e) {
        _animator.Play(FADE_IN);
    }

    private void Menu_OnHide(object sender, System.EventArgs e) {
        _animator.Play(FADE_OUT);
    }
}
