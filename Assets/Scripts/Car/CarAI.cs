using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CarAI : MonoBehaviour {

    public event EventHandler OnCarReached;

    private NavMeshAgent _carAgent;

    private void Awake() {

        _carAgent = GetComponent<NavMeshAgent>();
        SetDefaultAgentDestination(AIDestinationController.Instance.DefaultDropLocation);

    }

    private void SetDefaultAgentDestination(Vector3 _destination) {
        _carAgent.SetDestination(_destination);


    }
    private void Update() {
        // Now check if car reached the destination
        // Stop the agent if reached destination and spawn a customer next to it
        if (_carAgent.remainingDistance < 0.1f && !_carAgent.isStopped) {
            _carAgent.isStopped = true;
            AIDestinationController.Instance.OccupyDropPoint();
            OnCarReached?.Invoke(this, EventArgs.Empty);
        }
    }


}
