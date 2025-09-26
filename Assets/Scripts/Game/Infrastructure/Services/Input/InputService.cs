using UnityEngine;

namespace Game.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Jump = "Jump";
        protected const string MouseX = "MouseX";
        protected const string MouseY = "MouseY";

        public abstract Vector2 Axis { get; }
        public abstract Vector2 MouseAxis { get; }
        public bool IsJumpButtonUp => SimpleInput.GetButtonUp(Jump);


    }
}