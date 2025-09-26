using UnityEngine;

namespace Game.Infrastructure.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        Vector2 MouseAxis { get; }
        bool IsJumpButtonUp { get; }
    }
}