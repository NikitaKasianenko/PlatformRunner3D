using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public List<PositionOnLevel> PositionsOnLevels;
        public List<string> CheckPoints;

        public WorldData(string initialLevel)
        {
            PositionsOnLevels = new List<PositionOnLevel>();
            CheckPoints = new List<string>();
            PositionsOnLevels.Add(new PositionOnLevel(initialLevel));
        }

        public Vector3Data GetPositionForLevel(string level)
        {
            var p = PositionsOnLevels.FirstOrDefault(x => x.Level == level);
            return p?.Position;
        }

        public void SetPositionForLevel(string level, Vector3Data position)
        {
            var p = PositionsOnLevels.FirstOrDefault(x => x.Level == level);
            if (p != null)
                p.Position = position;
            else
                PositionsOnLevels.Add(new PositionOnLevel(level, position));
        }
    }
}