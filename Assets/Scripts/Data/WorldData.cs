using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public List<string> CheckPoints;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            CheckPoints = new List<string>();
        }
    }
}