using System;
using DG.Tweening;
using UnityEngine;
using Game.Checkpoint;
using Object = UnityEngine.Object;

namespace Game.Scene.Prize
{
    public class PrizeAnimation : MonoBehaviour,IAnimation
    {
        [SerializeField] private float _duration = 1.5f;
        [SerializeField] private float _moveUpDistance = 1f;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }


        public Tween Play()
        {
            var startPos = _transform.position;
            var targetPos = startPos + Vector3.up * _moveUpDistance;

            Sequence seq = DOTween.Sequence();

            seq.Append(_transform.DOMove(targetPos, _duration * 0.4f).SetEase(Ease.OutQuad));

            seq.Join(_transform.DORotate(new Vector3(0f, 360f, 0f), _duration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear));

            seq.Append(_transform.DOScale(Vector3.zero, _duration * 0.6f).SetEase(Ease.InBack));

            return seq;
        }

    }
}