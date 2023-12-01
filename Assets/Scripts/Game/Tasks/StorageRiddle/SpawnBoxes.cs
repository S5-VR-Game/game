using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Tasks.StorageRiddle
{
    public class SpawnBoxes : MonoBehaviour
    {
        public GameObject boxPrefab;
        
        [HideInInspector]
        public int maxAmountDeliveryBoxes;
        
        public Transform spawnPos1;
        public Transform spawnPos2;
        public Transform spawnPos3;
        public Transform spawnPos4;
        public Transform spawnPos5;
        public Transform spawnPos6;
        public Transform spawnPos7;
        public Transform spawnPos8;
        public Transform spawnPos9;
        public Transform spawnPos10;
        public Transform spawnPos11;
        public Transform spawnPos12;
        private readonly List<Transform> _possibleSpawnPositions = new();
        
        private void Start()
        {
            _possibleSpawnPositions.Add(spawnPos1);
            _possibleSpawnPositions.Add(spawnPos2);
            _possibleSpawnPositions.Add(spawnPos3);
            _possibleSpawnPositions.Add(spawnPos4);
            _possibleSpawnPositions.Add(spawnPos5);
            _possibleSpawnPositions.Add(spawnPos6);
            _possibleSpawnPositions.Add(spawnPos7);
            _possibleSpawnPositions.Add(spawnPos8);
            _possibleSpawnPositions.Add(spawnPos9);
            _possibleSpawnPositions.Add(spawnPos10);
            _possibleSpawnPositions.Add(spawnPos11);
            _possibleSpawnPositions.Add(spawnPos12);
            
            for (var i = 0; i < maxAmountDeliveryBoxes; i++)
            {
                var boxPosition = _possibleSpawnPositions[Random.Range(0, _possibleSpawnPositions.Count)];
                
                _possibleSpawnPositions.Remove(boxPosition);
                
                Instantiate(boxPrefab, boxPosition.position, new Quaternion());
                Debug.Log("Box spawned");
            }
        }
    }
    
}