using System.Collections.Generic;
using UnityEngine;

namespace Game.Tasks.StorageRiddle
{
    public class SpawnBoxes : MonoBehaviour
    {
        public GameObject boxPrefab;
        
        public int maxAmountDeliveryBoxes;
        
        public static Transform SpawnPos1;
        public static Transform SpawnPos2;
        public static Transform SpawnPos3;
        public static Transform SpawnPos4;
        public static Transform SpawnPos5;
        public static Transform SpawnPos6;
        public static Transform SpawnPos7;
        public static Transform SpawnPos8;
        public static Transform SpawnPos9;
        public static Transform SpawnPos10;
        public static Transform SpawnPos11;
        public static Transform SpawnPos12;
        private readonly List<Transform> _possibleSpawnPositions = new (){ SpawnPos1, SpawnPos2, SpawnPos3, SpawnPos4, 
            SpawnPos5, SpawnPos6, SpawnPos7, SpawnPos8, SpawnPos9, SpawnPos10, SpawnPos11, SpawnPos12};
        
        
        private void Start()
        {
            for (var i = 0; i < maxAmountDeliveryBoxes; i++)
            {
                var boxPosition = GetRandomBoxSpawn();
                _possibleSpawnPositions.Remove(boxPosition);

                Instantiate(boxPrefab, boxPosition.position, new Quaternion());
            }
        }
        
        
        private Transform GetRandomBoxSpawn()
        {
            return _possibleSpawnPositions[Random.Range(0, _possibleSpawnPositions.Count)];
        }
    }
    
}