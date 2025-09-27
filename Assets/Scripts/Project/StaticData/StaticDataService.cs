using Data;
using UnityEngine;

namespace Project.StaticData
{
    [CreateAssetMenu(fileName = "StaticDataService", menuName = "Data/StaticDataService")]
    public class StaticDataService : ScriptableObject, IStaticDataService 
    {
        public GameSettings GameSettings => _gameSettings;
        [SerializeField] private GameSettings _gameSettings;
    }
}