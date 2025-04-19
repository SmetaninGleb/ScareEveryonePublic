using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PeacefulMobAnimComponent : MonoBehaviour
{
    [SerializeField]
    private string _walkBoolName;
    [SerializeField]
    private string _runBoolName;
    [SerializeField]
    private string _comaTriggerName;
    [SerializeField]
    private string[] _angryTriggerNameList;
    [SerializeField]
    private string[] _destroyTriggerNameList;
    [SerializeField]
    private string[] _scaredTriggerNameList;
    private Animator _animator;

    public void InitializeVariables()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartWalkAnim()
    {
        _animator.SetBool(_walkBoolName, true);
    }

    public void StopWalkAnim()
    {
        _animator.SetBool(_walkBoolName, false);
    }

    public void StartRunAnim()
    {
        _animator.SetBool(_runBoolName, true);
    }

    public void StopRunAnim()
    {
        _animator.SetBool(_runBoolName, false);
    }

    public void StartScareAnimation()
    {
        string randomScareTriggerName = _scaredTriggerNameList[Random.Range(0, _scaredTriggerNameList.Length)];
        _animator.SetTrigger(randomScareTriggerName);
    }

    public void StartComaAnimation()
    {
        _animator.SetTrigger(_comaTriggerName);
    }

    public void StartAngryAnimation()
    {
        string randomAngryTriggerName = _angryTriggerNameList[Random.Range(0, _angryTriggerNameList.Length)];
        _animator.SetTrigger(randomAngryTriggerName);
    }

    public void StartDestroyAnimation()
    {
        string randomDestroyTriggerName = _destroyTriggerNameList[Random.Range(0, _destroyTriggerNameList.Length)];
        _animator.SetTrigger(randomDestroyTriggerName);
    }
}
