using System.Collections.Generic;
using Game.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Game.Infrastructure.Factory
{
    public interface IGameFactory
    {
        GameObject CreatePlayer(Vector3 at);
    }
}