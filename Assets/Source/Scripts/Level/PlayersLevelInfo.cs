using System;

namespace Source.Scripts.Level
{
    [Serializable]
    public struct PlayersLevelInfo
    {
        public int exp;
        public int level;

        public PlayersLevelInfo(int playersExp = 0, int playersLevel = -1)
        {
            exp = playersExp;
            level = playersLevel;
        }
    }
}