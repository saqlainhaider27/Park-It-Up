using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private const string SPEED = "Speed";
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetFloat(SPEED, Player.Instance.Speed);
    }
}
