using System;
using UnityEngine;

public class NPC : MonoBehaviour {

    private NPCAI _NPCAI;

    [field:SerializeField]
    public NPCSO NPCSO {
        get; private set;
    }
    public NPCStates CurrentState {
        get; set;
    }
    private void Awake() {
        _NPCAI = GetComponent<NPCAI>();
    }
    private void Start() {
        _NPCAI.OnNPCReached += NPCAI_OnNPCReached;
    }

    private void NPCAI_OnNPCReached(object sender, EventArgs e) {
        // Check if NPC reached entry or exitpoint
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
    public void Show() {
        gameObject.SetActive(true);
    }

}
