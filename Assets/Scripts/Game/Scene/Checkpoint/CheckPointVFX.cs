using UnityEngine;

namespace Game.Scene.Checkpoint
{
    public class CheckPointVFX : IVFX
    {
        private ParticleSystem  _particleSystem;

        public CheckPointVFX(ParticleSystem particleSystem)
        {
            _particleSystem = particleSystem;
        }

        public void Play()
        {
            _particleSystem.Play();
        }
    }
}