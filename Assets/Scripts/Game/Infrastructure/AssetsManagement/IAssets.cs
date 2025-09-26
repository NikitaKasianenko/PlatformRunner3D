using UnityEngine;

namespace Game.Infrastructure.AssetsManagement
{
    public interface IAssets
    {
        GameObject Load(string path);
    }
}