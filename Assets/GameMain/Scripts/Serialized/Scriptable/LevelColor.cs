using UnityEngine;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
namespace Fishing
{
    [CreateAssetMenu(fileName = "New LevelColorList", menuName = "Scriptable/Create LevelColorList", order = 2)]
    public class LevelColor : ScriptableObject
    {
        [SerializeField]
        private Color m_None,m_Level1, m_Level2, m_Level3, m_Level4, m_Level5;
        public Color GetLevelColor(int level)
        {
            if (level > 5 || level < 0) Log.Warning("value out range!");
            switch (level)
            {
                case 0: return m_None;
                case 1: return m_Level1;
                case 2: return m_Level2;
                case 3: return m_Level3;
                case 4: return m_Level4;
                case 5: return m_Level5;
                default: return Color.black;
            }
        }
    }
}