using System;
using Data;
using Game.Infrastructure.Services;
using Game.Infrastructure.Services.Input;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private float _playerSpeed = 5.0f;
        [SerializeField] private float _rotationSpeed = 540.0f;
        [SerializeField] public float _jumpHeight;
        [SerializeField] public float _gravity;
        
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundLayer;

    
        private IInputService _inputService;
        private CharacterController _characterController;
        private Camera _mainCamera;

        private Vector3 velocity;
        
        [HideInInspector] public float planarSpeed;
        [HideInInspector] public bool isGrounded;
        [HideInInspector] public float verticalSpeed;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(IInputService inputService, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _inputService = inputService;
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            IsGroundedCustom();
            HandleJump();
            HandleMove();
        }

        private void HandleJump()
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (isGrounded && _inputService.IsJumpButtonUp)
            {
                velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }

            velocity.y += _gravity * Time.deltaTime;
            
        }

        private void HandleMove()
        {
            Vector2 axis = _inputService.Axis;
            Vector3 cameraForward = _mainCamera.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 cameraRight = _mainCamera.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            Vector3 moveDirection = (cameraForward * axis.y + cameraRight * axis.x);

            _characterController.Move((moveDirection * _playerSpeed + velocity) * Time.deltaTime);


            if (moveDirection.sqrMagnitude > Constants.Epsilon)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    _rotationSpeed * Time.deltaTime
                );
            }
            var planarMovement = _characterController.velocity;
            planarMovement.y = 0;
            planarSpeed = planarMovement.magnitude;
            verticalSpeed = _characterController.velocity.y;
            
        }
        
        void  IsGroundedCustom()
        {
            isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }
        



        public void LoadProgress(PlayerProgress progress)
        {
            
            Debug.Log(progress.ToJson());
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                {
                    Warp(to: savedPosition);
                }
            }
        }

        public void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector();
            _characterController.enabled = true;
        }

        private string CurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.DeadZoneTag))
            {
                _signalBus.Fire(new PlayerDiedSignal(gameObject));
            }
            
        }
    }
}
