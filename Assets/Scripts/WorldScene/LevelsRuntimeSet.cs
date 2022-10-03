using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace TripleTriad.WorldScene
{
    [Serializable]
    [CreateAssetMenu(fileName = "Runtime MapLevelData Set", menuName = "TripleTriad/MapLevelData Runtime Set")]
    public class LevelsRuntimeSet : RuntimeSet<MapLevelData>
    {
        public string WorldMapName;

        public MapLevelData GetCurrentLevel()
        {
            return Items.First(i => i.IsVisited == false);
        }
        
        public MapNodeData GetCurrentBattle()
        {
            return GetCurrentLevel().CurrentNode;
        }

        public void Reset()
        {
            foreach (MapLevelData level in Items)
            {
                level.IsVisited = false;
                level.Reset();
            }
        }

        public void InitFrom(LevelsRuntimeSet starterWorldMap)
        {
            Items = starterWorldMap.Items;
            Reset();
        }

        public bool IsLastLevel(MapLevelData runtimeLevel)
        {
            var lastLevel = Items.Last();
            return lastLevel.LevelName == runtimeLevel.LevelName;
        }

        public void Visits(string runtimeLevelLevelName)
        {
            var visitedLevel = Items.First(l => l.LevelName == runtimeLevelLevelName);
            visitedLevel.IsVisited = true;
        }

        public bool IsComplete()
        {
            return Items.All(l => l.IsVisited);
        }
    }
}