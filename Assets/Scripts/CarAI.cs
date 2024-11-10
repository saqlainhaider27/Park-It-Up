using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CarAI : MonoBehaviour {

    private PrometeoCarController _carController;

    [field: SerializeField]
    public CarAISO CarAISO {
        get; private set;
    }
    private NavMeshAgent _carAgent;

    private void Awake() {
        _carController = GetComponent<PrometeoCarController>();
        // Diable the car controller and only enable when player is in the car
        _carController.enabled = false;
        _carAgent = GetComponent<NavMeshAgent>();
        SetDefaultAgentDestination(CarAIController.Instance.DefaultDropLocation);

    }

    private void SetDefaultAgentDestination(Vector3 _destination) {
        _carAgent.SetDestination(_destination);


    }
    private void Update() {
        // Now check if car reached the destination
        // Stop the agent if reached destination and spawn a customer next to it
        if (_carAgent.remainingDistance < 0.1f && !_carAgent.isStopped) {
            _carAgent.isStopped = true;
            CarAIController.Instance.OccupyDropPoint();
            // TODO: Spawns CharacterAI
        }
    }
}
