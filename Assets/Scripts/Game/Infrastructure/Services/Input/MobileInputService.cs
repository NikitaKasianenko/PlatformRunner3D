using SimpleInputNamespace;
using UnityEngine;

namespace Game.Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        
        private readonly Touchpad _touchpad;
        private readonly Joystick _joystick;

        public MobileInputService(Touchpad touchpad,Joystick joystick)
        {
            _touchpad = touchpad;
            _joystick = joystick;
        }
        public override Vector2 Axis => _joystick.Value;

        public override Vector2 MouseAxis => _touchpad.Value;
    }
}