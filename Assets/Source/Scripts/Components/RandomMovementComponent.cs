using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomMovementComponent : MonoBehaviour
{
    [SerializeField]
    private bool _doNotMove;
    [SerializeField]
    [MinMaxSlider(1f, 10f)]
    private Vector2 _gameBeginWaitingTimeRange;
    [ShowNonSerializedField]
    private bool _isWaiting;
    [ShowNonSerializedField]
    private bool _isMoving;
    [ShowNonSerializedField]
    private bool _isReadyToMove;
    [ShowNonSerializedField]
    private bool _isReadyToWait;
    [ShowNonSerializedField]
    private float _waitingTime;
    [ShowNonSerializedField]
    private float _startWaitingTime;
    private NavMeshAgent _navMeshAgent;
    [ShowNonSerializedField]
    private RandomMovementDestinationComponent _currentDestination;
    [ShowNonSerializedField]
    private bool _isMovingBlocked;
    private float _roundnessMistake = 0.01f;
    private bool _isWaitingFirst = true;

    public bool IsMovingBlocked => _isMovingBlocked;
    public bool IsReadyToMove => _isReadyToMove;
    public bool IsReadyToWait => _isReadyToWait;
    public bool DoNotMove => _doNotMove;
    public RandomMovementDestinationComponent CurrentDestination => _currentDestination;

    public void UpdateValues()
    {
        if (_isWaiting && Time.timeSinceLevelLoad - _startWaitingTime >= _waitingTime)
        {
            _isWaiting = false;
            _isReadyToMove = true;
        }
        if (_currentDestination == null) return;
        Vector3 currentDestPosition = _currentDestination.transform.position;
        if (_isMoving && Mathf.Abs(transform.position.x - currentDestPosition.x) < _roundnessMistake && Mathf.Abs(transform.position.z - currentDestPosition.z) < _roundnessMistake)
        {
            _isMoving = false;
            _isReadyToWait = true;
        }
    }

    public bool IsWaiting
    {
        get
        {
            return _isWaiting;
        }
    }

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
    }

    public void InitializeValues()
    {
        _isWaiting = false;
        _isMoving = false;
        _isReadyToMove = false;
        _isReadyToWait = false;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_isWaitingFirst)
        {
            _startWaitingTime = Time.timeSinceLevelLoad;
            float waitingTime = Random.Range(_gameBeginWaitingTimeRange.x, _gameBeginWaitingTimeRange.y);
            StartWaiting(waitingTime);
            _isWaitingFirst = false;
        } else
        {
            _isReadyToMove = true;
        }
    }

    public void BlockMoving()
    {
        _isMovingBlocked = true;
    }

    public void UnblockMoving()
    {
        InitializeValues();
        _isMovingBlocked = false;
    }

    public void StartMoving(RandomMovementDestinationComponent destination)
    {
        if (IsWaiting)
        {
            Debug.LogError("Mob cannot start moving. It is in waiting mod.");
            return;
        }
        if (IsMovingBlocked)
        {
            Debug.LogError("Mob cannot start moving. Moving is blocked.");
            return;
        }
        if (_currentDestination != null) _currentDestination.Release();
        _currentDestination = destination;
        _currentDestination.Occupy();
        _navMeshAgent.SetDestination(destination.transform.position);
        _isMoving = true;
        _isReadyToMove = false;
    }
    
    public void StartWaiting(float waitingTime)
    {
        if (IsMoving)
        {
            Debug.LogError("Mob cannot start waiting. It is in moving mod.");
            return;
        }
        if (IsMovingBlocked)
        {
            Debug.LogError("Mob cannot start waiting. Moving is blocked.");
            return;
        }
        _waitingTime = waitingTime;
        _startWaitingTime = Time.timeSinceLevelLoad;
        _isWaiting = true;
        _isReadyToWait = false;
    }

    public void ReleaseCurrentDestination()
    {
        if (!_isMovingBlocked || _currentDestination == null) return;
        _currentDestination.Release();
    }
}
