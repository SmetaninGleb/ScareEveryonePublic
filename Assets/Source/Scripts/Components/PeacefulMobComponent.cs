using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(RandomMovementComponent))]
[RequireComponent(typeof(VisabilityComponent))]
[RequireComponent(typeof(PeacefulMobAnimComponent))]
[RequireComponent(typeof(AudioSource))]
public class PeacefulMobComponent : MonoBehaviour
{
    [Required]
    [SerializeField]
    private AudioClip _angryAudioClip;
    private AudioSource _audioSouce;
    private HidePlaceComponent hidePlaceToDestroy;
    private bool _isSoundOn;
    private bool _isVibroOn;
    [Required]
    [SerializeField]
    private MobEmotionComponent _emotionComponent;

    [SerializeField]
    private float _runningSpeed;
    [SerializeField]
    private float _maxRunDistance = 100f;
    private float _walkSpeed;
    private bool _isRunning;
    private bool _isPassedOut;

    [HideInInspector]
    public float MaxRunDistance => _maxRunDistance;
    [HideInInspector]
    public bool IsRunning => _isRunning;
    [HideInInspector]
    public bool IsPassedOut => _isPassedOut;
    [HideInInspector]
    public NavMeshAgent NavMeshAgent;
    [HideInInspector]
    public RandomMovementComponent RandomMovementComponent;
    [HideInInspector]
    public PeacefulMobAnimComponent AnimComponent;
    [HideInInspector]
    public Rigidbody Rigidbody;
    [HideInInspector]
    public VisabilityComponent VisabilityComponent;
    [ReadOnly]
    public bool IsAngry;
    [ReadOnly]
    public bool IsBecameAngry;
    [ReadOnly]
    public bool IsGoToDestroy;
    [ReadOnly]
    public bool IsScared;

    public void InitializeVariables()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        RandomMovementComponent = GetComponent<RandomMovementComponent>();
        AnimComponent = GetComponent<PeacefulMobAnimComponent>();
        VisabilityComponent = GetComponent<VisabilityComponent>();
        Rigidbody = GetComponent<Rigidbody>();
        _audioSouce = GetComponent<AudioSource>();
        _walkSpeed = NavMeshAgent.speed;
        _emotionComponent.InitializeValues();
        Rigidbody.isKinematic = true;
        IsAngry = false;
        IsBecameAngry = false;
        IsGoToDestroy = false;
        IsScared = false;
        _isRunning = false;
        _isPassedOut = false;
    }

    public void BecomeScared()
    {
        AnimComponent.StartScareAnimation();
        RandomMovementComponent.BlockMoving();
        NavMeshAgent.enabled = false;
        IsAngry = false;
        IsScared = true;
        _emotionComponent.ShowScaryEmotion();
    }

    public void PassOut()
    {
        AnimComponent.StartComaAnimation();
        GetComponent<Collider>().enabled = false;
        VisabilityComponent.TurnOffDraws();
        RandomMovementComponent.ReleaseCurrentDestination();
        _emotionComponent.ShowPassOutEmotion();
        _isPassedOut = true;
    }

    public void StoppedAngryAnimation()
    {
        NavMeshAgent.isStopped = false;
        NavMeshAgent.SetDestination(hidePlaceToDestroy.transform.position);
        IsGoToDestroy = true;
    }

    public void BecomeAngry(HidePlaceComponent hidePlaceToDestroy)
    {
        if (hidePlaceToDestroy == null)
        {
            IsAngry = false;
            return;
        }
        RandomMovementComponent.BlockMoving();
        this.hidePlaceToDestroy = hidePlaceToDestroy;
        AnimComponent.StartAngryAnimation();
        IsBecameAngry = true;
        NavMeshAgent.isStopped = true;
        hidePlaceToDestroy.ShouldBeDestroyed = true;
        _emotionComponent.ShowAngryEmotion();
        if (_isSoundOn)
        {
            _audioSouce.clip = _angryAudioClip;
            _audioSouce.Play();
        }
    }

    public void StoppedDestroyingAnimation()
    {
        IsGoToDestroy = false;
        IsAngry = false;
        IsBecameAngry = false;
        RandomMovementComponent.UnblockMoving();
        NavMeshAgent.isStopped = false;
    }

    public void DestroyHidePlace(HidePlaceComponent hidePlace)
    {
        hidePlace.SetVibro(_isVibroOn);
        hidePlace.SetIsSoundOn(_isSoundOn);
        hidePlace.DestroyPlace();
        AnimComponent.StartDestroyAnimation();
        NavMeshAgent.isStopped = true;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (!IsAngry || IsScared) return;
        if (collider.TryGetComponent(out HidePlaceComponent hidePlace) && hidePlace == hidePlaceToDestroy)
        {
            DestroyHidePlace(hidePlace);
        } else
        {
            return;
        }
    }
    
    public void StartRunning(Vector3 destinationPosition)
    {
        if (RandomMovementComponent.CurrentDestination != null) RandomMovementComponent.CurrentDestination.Release();
        RandomMovementComponent.BlockMoving();
        NavMeshAgent.speed = _runningSpeed;
        AnimComponent.StopWalkAnim();
        AnimComponent.StartRunAnim();
        NavMeshAgent.SetDestination(destinationPosition);
        _isRunning = true;
        _emotionComponent.ShowScaryEmotion();
    }

    private void Update()
    {
        //Stop running check
        if (_isRunning && NavMeshAgent.velocity == Vector3.zero)
        {
            NavMeshAgent.speed = _walkSpeed;
            AnimComponent.StopRunAnim();
            RandomMovementComponent.UnblockMoving();
            _isRunning = false;
        }
    }

    public void RotateToTransform(Transform targetTransform)
    {
        NavMeshAgent.updateRotation = false;
        float rotateDuration = Quaternion.FromToRotation(transform.forward, targetTransform.position - transform.position).eulerAngles.y / NavMeshAgent.angularSpeed;
        transform.DORotateQuaternion(Quaternion.LookRotation(targetTransform.position - transform.position),  rotateDuration).onComplete = () => { NavMeshAgent.updateRotation = true; };
    }

    public void SetIsSoundOn(bool isSoundOn)
    {
        _isSoundOn = isSoundOn;
    }

    public void SetIsVibroOn(bool isVibroOn)
    {
        _isVibroOn = isVibroOn;
    }
}
