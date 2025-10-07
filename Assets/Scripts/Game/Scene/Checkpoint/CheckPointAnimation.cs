using DG.Tweening;
using Game.Checkpoint;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace Game.Scene.Checkpoint
{
    public class CheckPointAnimation : MonoBehaviour, IAnimation
    {
        [Header("Jump")]
        [SerializeField] private float jumpPower = 1f;
        [SerializeField] private int jumpCount = 1;
        [SerializeField] private float jumpDuration = 0.5f;

        [Header("Shrink")]
        [SerializeField] private float shrinkDuration = 0.25f;
        [SerializeField] private Ease shrinkEase = Ease.InBack;
        
        private Sequence _sequence;

        private Tween _currentTween;

        public Tween Play()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOJump(transform.position, jumpPower, jumpCount, jumpDuration).SetEase(Ease.OutQuad));
            _sequence.Append(transform.DOScale(Vector3.zero, shrinkDuration).SetEase(shrinkEase));
            _sequence.OnKill(() => _sequence = null);
            _sequence.Play();
            return _sequence;
        }

        private void OnDisable()
        {
            _currentTween?.Kill();
            _currentTween = null;
        }
    }
}