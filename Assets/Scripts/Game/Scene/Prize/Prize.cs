using System.Collections;
using DG.Tweening;
using Game.Checkpoint;
using Game.Scene.Checkpoint;
using UnityEngine;

namespace Game.Scene.Prize
{
    public class Prize : CheckPointBase
    {
        [SerializeField] private string persistentId = "0";
        private IAnimation  _prizeAnimation;

        private void Start()
        {
            _prizeAnimation = GetComponent<IAnimation>();
            _composeCollectedKey = $"{gameObject.scene.name}:{persistentId}";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.Player))
                return;
            HandleSave();
            StartCoroutine(PrizeCoroutine());
        }

        private IEnumerator PrizeCoroutine()
        {
            var tween = _prizeAnimation.Play();
            yield return new WaitUntil(() => tween.IsActive() == false || tween.IsComplete());
            DisableSelf();
        }
        
    }
}