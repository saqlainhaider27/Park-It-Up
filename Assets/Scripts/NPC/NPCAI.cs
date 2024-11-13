using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCAI : MonoBehaviour {
    
    private NavMeshAgent agent;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }


}
