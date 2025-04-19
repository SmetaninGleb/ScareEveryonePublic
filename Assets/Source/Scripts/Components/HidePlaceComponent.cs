using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;
using MoreMountains.NiceVibrations;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(AudioSource))]
public class HidePlaceComponent : MonoBehaviour
{
    [SerializeField]
    [Required]
    private ScareSpawnComponent _scareSpawner;
    [SerializeField]
    private RandomMovementDestinationComponent _nearRandomDestination;
    [Required]
    [SerializeField]
    private ParticleSystem _destroyParticle;
    [SerializeField]
    private GarbageComponent _garbage;
    private ScareAnimComponent _scareAnim;
    [Required]
    [SerializeField]
    private HidePlacePointerComponent _pointer;
    private Material _standardMaterial;
    private Renderer _renderer;
    private bool _isVibroOn;
    private bool _isSoundOn;
    private AudioSource _audioSource;
    [Foldout("Audio")]
    [SerializeField]
    [Required]
    private AudioClip _scareAudioClip;
    [Foldout("Audio")]
    [SerializeField]
    [Required]
    private AudioClip _destroyAudioClip;
    private NavMeshObstacle _navMeshObstacle;

    public float ImpulseForceMagnitude;
    [HideInInspector]
    public bool IsDestroyed;
    [HideInInspector]
    public bool ShouldBeDestroyed;
    [HideInInspector]
    public Rigidbody Rigidbody;
    [HideInInspector]
    public bool IsHighlighted;
    public ScareSpawnComponent ScareSpawner => _scareSpawner;
    public NavMeshObstacle NavMeshObstacle => _navMeshObstacle;

    public void InitializeVariables()
    {
        IsDestroyed = false;
        ShouldBeDestroyed = false;
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _navMeshObstacle.carving = true;
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<Collider>().isTrigger = true;
        if (GetComponent<Renderer>())
        {
            _renderer = GetComponent<Renderer>();
            _standardMaterial = GetComponent<Renderer>().material;
        } else
        {
            _renderer = GetComponentInChildren<Renderer>();
            _standardMaterial = GetComponentInChildren<Renderer>().material;
        }
        IsHighlighted = false;
        _scareAnim = GetComponentInChildren<ScareAnimComponent>();
        _scareAnim.InitializeValues();
        _audioSource = GetComponent<AudioSource>();
        _garbage.InitializeValues();
        if (_pointer != null) _pointer.InitializeValues();
    }

    public void DestroyPlace()
    {
        //Rigidbody.constraints = RigidbodyConstraints.None;
        //Rigidbody.AddForce(ImpulseForceMagnitude * Vector3.up, ForceMode.Impulse);
        //GetComponent<Collider>().isTrigger = false;
        _renderer.enabled = false;
        IsDestroyed = true;
        _garbage.AppearGarbage();
        _destroyParticle.Play();
        if (_isVibroOn)
        {
            MMVibrationManager.Haptic(HapticTypes.RigidImpact);
        }
        if (_isSoundOn)
        {
            _audioSource.Stop();
            _audioSource.clip = _destroyAudioClip;
            _audioSource.Play();
        }
    }
    
    public bool CanRemoveNearDestination()
    {
        return _nearRandomDestination != null && !_nearRandomDestination.IsOccupied && !_nearRandomDestination.IsBlocked;
    }

    public void RemoveNearRandomDestination()
    {
        if (CanRemoveNearDestination())
        {
            _nearRandomDestination.Block();
            _nearRandomDestination.gameObject.SetActive(false);
            Debug.Log("Destination " + _nearRandomDestination.gameObject.name + " removed.");
        } else
        {
            Debug.LogError("Cannot remove near destination.");
        }
    }

    public void HighlightWithMatrial(Material newMaterial)
    {
        _renderer.material = newMaterial;
    }

    public void ShowPointer()
    {
        _pointer.ShowPointer();
    }

    public void DisablePointer()
    {
        _pointer.DisablePointer();
    }

    public void StopHighlighting()
    {
        _renderer.material = _standardMaterial;
    }

    public void StartScare()
    {
        _scareAnim.StartScareAnimation();
        if (_isSoundOn)
        {
            _audioSource.Stop();
            _audioSource.clip = _scareAudioClip;
            _audioSource.Play();
        }
    }

    public void AddScaredMob(PeacefulMobComponent mob)
    {
        _scareAnim.ScaredMobs.Add(mob);
    }

    public void SetScaryingMonster(HideableComponent monster)
    {
        _scareAnim.ScareMonster = monster;
    }

    public void SetVibro(bool isVibroOn)
    {
        _isVibroOn = isVibroOn;
    }

    public void SetIsSoundOn(bool isSoundOn)
    {
        _isSoundOn = isSoundOn;
    }
}
