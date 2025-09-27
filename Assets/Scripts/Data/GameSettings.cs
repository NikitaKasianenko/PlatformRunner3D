using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameSettings",menuName = "Data/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public Vector2 Sensitivity = new Vector2(1f, 1f);
        public float RespawnDuration = 5f;
    }
}