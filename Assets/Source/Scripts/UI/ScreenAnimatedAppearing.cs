
using UnityEngine;
using DG.Tweening;

class ScreenAnimatedAppearing : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _confettiParticle;
    [SerializeField]
    private float _appearingDuration = 0.3f;
    [SerializeField]
    private Vector3 _appearingPunch = new Vector3(0.7f, 0.7f, 0.7f);
    [SerializeField]
    private int _appearingVibrato = 5;

    private void OnEnable()
    {
        if (_confettiParticle != null) _confettiParticle.Play();
        transform.DOPunchScale(_appearingPunch, _appearingDuration, vibrato:_appearingVibrato);
    }
}