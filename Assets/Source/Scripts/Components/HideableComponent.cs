using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HideableComponent : MonoBehaviour
{
    [Required]
    [SerializeField]
    private ParticleSystem _poofParticles;

    public ParticleSystem PoofParticles => _poofParticles;
    public float ScareRadius;
    [HideInInspector]
    public bool IsHiding;
    [HideInInspector]
    public bool IsTouchingHidePlace;
    [HideInInspector]
    public HidePlaceComponent TouchingHidePlace;
    [HideInInspector]
    public HidePlaceComponent HidingPlace;
    [HideInInspector]
    public bool IsAppearing;
    public GameObject RadiusSprite;
    public VisionDrawComponent RadiusDraw;
    
    public event Action<HideableComponent, HidePlaceComponent> TouchedEvent;
    public event Action<HideableComponent, HidePlaceComponent> UntouchedEvent;

    public void InitializeVariables()
    {
        RadiusSprite.SetActive(false);
        RadiusDraw.DrawVision(360f, ScareRadius);
        RadiusDraw.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out HidePlaceComponent hidePlace))
        {
            IsTouchingHidePlace = true;
            TouchingHidePlace = hidePlace;
            TouchedEvent?.Invoke(this, hidePlace);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out HidePlaceComponent hidePlace))
        {
            IsTouchingHidePlace = false;
            UntouchedEvent?.Invoke(this, hidePlace);
        }
    }

    private void OnValidate()
    {
        RadiusDraw.DrawVision(360f, ScareRadius);
        RadiusSprite.transform.localScale = new Vector3(2*ScareRadius, 2*ScareRadius, 1f);
    }

    public bool CanScare(PeacefulMobComponent mob)
    {
        if (mob.IsScared) return false;
        float distanceToMob = (mob.transform.position - HidingPlace.transform.position).magnitude;
        if (distanceToMob <= ScareRadius)
        {
            Vector3 hidePlacePos = HidingPlace.transform.position;
            Ray playerToMobRay = new Ray(hidePlacePos, mob.transform.position - hidePlacePos);
            RaycastHit[] hits = Physics.RaycastAll(playerToMobRay, distanceToMob, mob.gameObject.layer);
            if (hits.Length == 0)
            {
                return true;
            }
        }
        return false;
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Appear()
    {
        gameObject.SetActive(true);
    }
}
