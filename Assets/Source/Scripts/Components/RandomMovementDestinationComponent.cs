using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RandomMovementDestinationComponent : MonoBehaviour
{
    private bool _isOccupied;
    private bool _isBlocked;
    [SerializeField]
    [MinMaxSlider(1f, 10f)]
    private Vector2 _waitingTimeRange;

    public bool IsOccupied => _isOccupied;
    public bool IsBlocked => _isBlocked;
    public Vector2 WaitingTimeRange => _waitingTimeRange;

    public void Occupy()
    {
        _isOccupied = true;
    }

    public void Release()
    {
        _isOccupied = false;
    }

    public void Block()
    {
        _isBlocked = true;
    }
}
