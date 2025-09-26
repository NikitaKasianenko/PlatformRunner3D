using UnityEngine;

namespace Game.CameraLogic
{
    public interface ICameraFollow
    {
        void Follow(Transform target);
    }
}