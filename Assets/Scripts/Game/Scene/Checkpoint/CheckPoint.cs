using System.Collections;
using Data;
using DG.Tweening;
using Game.Checkpoint;
using Game.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Game.Scene.Checkpoint
{
    public class CheckPoint : CheckPointBase, ISaveProgressReader
    {
        [SerializeField] private string persistentId = "0";
        [SerializeField] private ParticleSystem _particleSystem;

        private Coroutine _checkpointRoutine;

        private IVFX  _vfx;
        private IAnimation _animation;
        private Collider _collider;


        private void Awake()
        {
            _composeCollectedKey = $"{gameObject.scene.name}:{persistentId}";
            _vfx = new CheckPointVFX(_particleSystem);
            _animation = GetComponentInChildren<IAnimation>();
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.Player))
                return;

            HandleSave();
            StartCoroutine(CheckpointCoroutine());
        }

        private IEnumerator CheckpointCoroutine()
        {
            _collider.enabled = false;
            Tween tween = _animation.Play();
            yield return new WaitUntil(() => tween.IsActive() == false || tween.IsComplete());
            _vfx?.Play();
            yield return new WaitForSeconds(_particleSystem.main.duration);
            DisableSelf();
            _checkpointRoutine = null;
        }

    }
}