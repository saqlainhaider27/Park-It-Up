using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NPCAnimator : MonoBehaviour {

    [SerializeField] private NPC _NPC;
    private Animator animator;
    private const string WALKING = "Walking";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool(WALKING, _NPC.CurrentState == NPCStates.Walking);
    }
}
