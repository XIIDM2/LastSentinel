using DG.Tweening;
using UnityEngine;

public class FloatingPotion : MonoBehaviour
{
    [SerializeField] private float _floatDistance = 0.5f;
    [SerializeField] private float _duration = 1.0f;
    [SerializeField] private Ease _easeType = Ease.InOutCubic;

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
    private void Start()
    {
        Vector3 startPosition = transform.position;

        transform.DOLocalMoveY(startPosition.y + _floatDistance, _duration).SetEase(_easeType).SetLoops(-1, LoopType.Yoyo);
    }
}
