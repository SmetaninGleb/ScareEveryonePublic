

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScareAnimComponent : MonoBehaviour
{
    [HideInInspector]
    public List<PeacefulMobComponent> ScaredMobs = new List<PeacefulMobComponent>();
    [HideInInspector]
    public HideableComponent ScareMonster;

    [SerializeField]
    private string ScareAnimTriggerName;
    [SerializeField]
    private string StopAnimTriggerName;
    private Animator _animator;
    private bool _isScaring = false;

    public void InitializeValues()
    {
        _animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void StartScareAnimation()
    {
        if (_isScaring) return;
        gameObject.SetActive(true);
        _animator.SetTrigger(ScareAnimTriggerName);
        _isScaring = true;
        foreach (PeacefulMobComponent mob in ScaredMobs)
        {
            mob.RotateToTransform(transform);
        }
    }
    public void StopScareAnimation()
    {
        _isScaring = false;
        gameObject.SetActive(false);
        ScaredMobs.Clear();
    }
}