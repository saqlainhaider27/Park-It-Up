using System;
using UnityEngine;

public class NPC : MonoBehaviour {

    [field:SerializeField]
    public NPCSO NPCSO {
        get; private set;
    }
    public NPCStates CurrentState {
        get; set;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
    public void Show() {
        gameObject.SetActive(true);
    }

}
