using System;
using UnityEngine;

namespace Source.Scripts.GameCore.Battle.UnitLogic.Data
{
    [Serializable]
    public class SpawnData
    {
        public SpawnData(string id, Vector3 spawnPoint)
        {
            cardID = id;
            x = spawnPoint.x;
            y = spawnPoint.y;
            z = spawnPoint.z;
        }
        
        public string cardID;
        public float x;
        public float y;
        public float z;
        public uint serverTime;
    }
}