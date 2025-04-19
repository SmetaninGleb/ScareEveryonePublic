using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class VisabilityComponent : MonoBehaviour
{
    [SerializeField]
    private float _visionDistance;
    [Range(0, 360)]
    [SerializeField]
    private float _visionAngle;
    [SerializeField]
    private float _feelDistance;
    [SerializeField]
    private VisionDrawComponent _visionDrawComponent;
    [SerializeField]
    private VisionDrawComponent _feelDrawComponent;

    public float VisionDistance => _visionDistance;
    public float VisionAngle => _visionAngle;

    private void OnValidate()
    {
        DrawVisions();
    }

    private void Start()
    {
        DrawVisions();
    }

    private void DrawVisions()
    {
        _feelDrawComponent.DrawVision(360f - _visionAngle, _feelDistance);
        _visionDrawComponent.DrawVision(_visionAngle, _visionDistance);
    }

    public void TurnOnDraws()
    {
        _visionDrawComponent.gameObject.SetActive(true);
        _feelDrawComponent.gameObject.SetActive(true);
    }

    public void TurnOffDraws()
    {
        _visionDrawComponent.gameObject.SetActive(false);
        _feelDrawComponent.gameObject.SetActive(false);
    }
    
    public bool CanFeel(Transform targetTranform)
    {
        if (Vector3.Distance(transform.position, targetTranform.position) > _feelDistance)
        {
            return false;
        }
        Vector3 toTargetVector = new Vector3(targetTranform.position.x - transform.position.x, 0f, targetTranform.position.z - transform.position.z);
        Ray toTargetRay = new Ray(transform.position, toTargetVector);
        RaycastHit hit;
        if (Physics.Raycast(toTargetRay, out hit, _feelDistance) && hit.transform == targetTranform)
        {
            return true;
        }
        return false;
    }

    public bool CanSee(Transform targetTransform)
    {
        Vector3 toTargetVector = new Vector3(targetTransform.transform.position.x - transform.position.x, 0f, targetTransform.transform.position.z - transform.position.z);
        if (toTargetVector.magnitude > _visionDistance)
        {
            return false;
        }
        float angleToPlayerVector = Vector3.Angle(transform.forward, toTargetVector);
        if (angleToPlayerVector <= _visionAngle / 2f)
        {
            Ray toPlayerRay = new Ray(transform.position, toTargetVector);
            RaycastHit hit;
            if (Physics.Raycast(toPlayerRay, out hit, _visionDistance)
                && hit.transform.gameObject.GetComponent<PlayerComponent>() != null)
            {
                return true;
            }
        }
        return false;
    }
}
