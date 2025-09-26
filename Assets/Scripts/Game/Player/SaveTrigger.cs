using System;
using Game.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;
        public BoxCollider boxCollider;

        private void OnDrawGizmos()
        {
            if (boxCollider == null)
            {
                return;
            }
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position + boxCollider.center, boxCollider.size);
        }
        
        [Inject]
        public void Construct(ISaveLoadService  saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Player))
            {
                _saveLoadService.SaveProgress();          
                Debug.Log("Player saved");
            }
        }

    }

    public class Coin
    {
        public Transform CheckpointPosition;
    }
}