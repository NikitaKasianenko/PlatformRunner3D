using UnityEngine;

namespace Game.Infrastructure.AssetsManagement
{
    public class AssetsProvider : IAssets
    {
        public GameObject Load(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}