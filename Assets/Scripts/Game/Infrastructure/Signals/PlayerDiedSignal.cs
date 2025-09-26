using UnityEngine;

namespace Game.Infrastructure.Signals
{
    public class PlayerDiedSignal
    {
        public GameObject PlayerGameObject;

        public PlayerDiedSignal(GameObject  player) => PlayerGameObject = player;
    }
}