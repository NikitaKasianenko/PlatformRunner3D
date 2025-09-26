using UnityEngine;

namespace Game.Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis => new(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

        public override Vector2 MouseAxis => new(UnityEngine.Input.GetAxis(MouseX), UnityEngine.Input.GetAxis(MouseY));
    }
}