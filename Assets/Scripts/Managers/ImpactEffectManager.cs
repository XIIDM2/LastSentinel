using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ImpactEffectManager : MonoBehaviour, IGameManager
{
    [SerializeField, Range(0.01f, 1.0f)] private float _hitStopDuration = 0.05f;
    [SerializeField, Range(0.1f, 1.0f)] private float _hitShakeForce = 1.0f;

    private Coroutine _hitStopCoroutine;

    public void HitStopEffect()
    {
        if (_hitStopCoroutine != null)
        {
            StopCoroutine(_hitStopCoroutine);
            _hitStopCoroutine = null;
        }

        if (Managers.ScenesMananger.IsGameEnded) return;

        _hitStopCoroutine = StartCoroutine(HitStopRoutine(_hitStopDuration));
    }    

    public void HitShakeEffect(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(_hitShakeForce);
    }

    private IEnumerator HitStopRoutine(float hitStopDuration)
    {
        Time.timeScale = 0.0f;

        yield return new WaitForSecondsRealtime(hitStopDuration);

        Time.timeScale = 1.0f;
        _hitStopCoroutine = null;
        
    }
}
