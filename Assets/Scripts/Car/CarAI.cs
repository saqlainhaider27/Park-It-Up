using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CarAI : MonoBehaviour {

    public event EventHandler OnCarReached;

    private NavMeshAgent _carAgent;
    private bool _isWaitingForObstacle;

    [SerializeField] private float obstacleDetectionDistance = 2f; // Distance to detect obstacles in front
    [SerializeField] private Vector3 boxCastHalfExtents = new Vector3(1f, 0.5f, 1f); // Size of the box in each direction
    [SerializeField] private LayerMask obstacleLayer; // Layer of obstacles
    private Vector3 _offset = new Vector3(0f,1f,0f);

    private void Awake() {
        _carAgent = GetComponent<NavMeshAgent>();
        _carAgent.SetDestination(AIDestinationController.Instance.DefaultDropLocation);
    }

    private void Update() {
        // Check for obstacles in front of the car
        DetectObstacle();
        
        if (_carAgent.remainingDistance < 0.1f && !_carAgent.isStopped) {
            _carAgent.isStopped = true;
            OnCarReached?.Invoke(this, EventArgs.Empty);
        }
    }

    private void DetectObstacle() {
        // Define the center of the box cast in front of the car
        Vector3 boxCastCenter = transform.position + transform.forward * obstacleDetectionDistance / 2;

        // Perform the BoxCast
        if (Physics.BoxCast(transform.position + _offset, boxCastHalfExtents, transform.forward, out RaycastHit hit, transform.rotation, obstacleDetectionDistance, obstacleLayer)) {
            // Stop the car if an obstacle is detected
            if (!_carAgent.isStopped) {
                _carAgent.isStopped = true;
                _isWaitingForObstacle = true;
            }
        }
        else {
            // Resume movement if no obstacle is detected
            if (_isWaitingForObstacle) {
                _carAgent.isStopped = false;
                _isWaitingForObstacle = false;
            }
        }

        // Optional: visualize the BoxCast in the Scene view
        Debug.DrawRay(boxCastCenter + _offset, transform.forward * obstacleDetectionDistance, Color.red);
    }
    public void UpdateDestination(Vector3 _destination) {
        _carAgent.isStopped = false;
        _carAgent.SetDestination(_destination);
    }
}
