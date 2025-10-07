using UnityEngine;

namespace Game.Checkpoint
{
    public class CheckPointVFX : IVFX
    {
        private readonly ParticleSystem _particleSystem;

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