using Cinemachine;
using Game.Infrastructure.Services;
using Game.Infrastructure.Services.Input;
using Game.Infrastructure.Services.PersistentProgress;
using Project.StaticData;
using UnityEngine;
using Zenject;

namespace Game.CameraLogic
{
    public class CameraFollow : ILateTickable, ICameraFollow
    {
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly IInputService _input;
        private readonly IPlayerSettingsService _playerSettingsService;
        private Transform _target;
        private float _rotationX;
        private float _rotationY;
        private CinemachineTransposer _transposer;

        public CameraFollow(CinemachineVirtualCamera virtualCamera, IInputService input, IPlayerSettingsService playerSettingsService)
        {
            _virtualCamera = virtualCamera;
            _input = input;
            _playerSettingsService = playerSettingsService;
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        public void Follow(Transform target)
        {
            _target = target;
            _virtualCamera.Follow = target;
            _virtualCamera.LookAt = target;
        }

        public void LateTick()
        {
            if (_target == null) return;

            _rotationX += _input.MouseAxis.x * _playerSettingsService.PlayerSettings.Sensitivity.x;
            _rotationY -= _input.MouseAxis.y *  _playerSettingsService.PlayerSettings.Sensitivity.y;
            _rotationY = Mathf.Clamp(_rotationY, -30f, 80f);
            var rotation = Quaternion.Euler(_rotationY, _rotationX, 0);
            _transposer.m_FollowOffset = rotation * new Vector3(0, 0, -5f); 
        }
    }
}