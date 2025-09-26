using System;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private CharacterController _controller;
        private PlayerMove _player;
        private Vector3 horizontalVelocity;
        private readonly int PlanarSpeedHash = Animator.StringToHash("PlanarSpeed");
        private readonly int VerticalSpeedHash = Animator.StringToHash("VerticalSpeed");
        private readonly int IsGroundedSpeedHash = Animator.StringToHash("Grounded");
        

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _player = GetComponent<PlayerMove>();
        }

        private void Update()
        {
            _animator.SetFloat(PlanarSpeedHash, _player.planarSpeed);
            _animator.SetFloat(VerticalSpeedHash, _player.verticalSpeed);
            _animator.SetBool(IsGroundedSpeedHash, _player.isGrounded);
            
        }
    }
}