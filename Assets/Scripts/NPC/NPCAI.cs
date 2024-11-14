using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCAI : MonoBehaviour {

    public event EventHandler OnNPCReached;
    private NPC _NPC;
    private NavMeshAgent _agent;
    
    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _NPC = GetComponent<NPC>();
        // Set the destination of npc to building entrypoint
        _agent.SetDestination(AIDestinationController.Instance.BuildingEntryPoint);
        _NPC.CurrentState = NPCStates.Walking;
    }

    private void Update() {
        // Now check if car reached the destination
        // Stop the agent if reached destination and spawn a customer next to it
        if (_agent.remainingDistance < 0.1f && !_agent.isStopped) {
            _agent.isStopped = true;
            _NPC.CurrentState = NPCStates.Idle;
            OnNPCReached?.Invoke(this, EventArgs.Empty);
        }
    }

    






}
