using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Serialization;

namespace Game.Player
{
    public class PlayerRagdoll : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody[] _ragdollBodies;
        private CharacterController _characterController;
        [SerializeField] public Transform transformToFollow;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _ragdollBodies = GetComponentsInChildren<Rigidbody>();
            _characterController = GetComponent<CharacterController>();
            if (!transformToFollow)
            {
                transformToFollow = transform;
            }
            SetRagdollEnabled(false);
        }

        public async Task ActivateRagdoll(float  duration)
        {
            _characterController.enabled = false;
            SetRagdollEnabled(true);
            
            await Task.Delay((int)(duration * 1000));
            
            SetRagdollEnabled(false);
            _characterController.enabled = true;

        }

        private void SetRagdollEnabled(bool state)
        {
            _animator.enabled = !state;
            foreach (Rigidbody rb in _ragdollBodies)
            {
                rb.isKinematic = !state;
            }
        }

    }
}