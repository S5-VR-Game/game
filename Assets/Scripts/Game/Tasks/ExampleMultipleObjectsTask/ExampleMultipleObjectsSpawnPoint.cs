using UnityEngine;

namespace Game.Tasks.ExampleMultipleObjectsTask
{
    public class ExampleMultipleObjectsSpawnPoint : TaskSpawnPoint
    {
        [SerializeField] public GameObject customSpawnPoint1;
        [SerializeField] public GameObject[] customSpawnPointList;
    }
}