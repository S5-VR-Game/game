using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Tasks.marble_gravity
{
    public class MarbleGravityFactory : GameTaskFactory<TaskSpawnPoint>
    {
        
        protected override GameTask CreateTask(TaskSpawnPoint spawnPoint)
        {
            throw new InvalidOperationException();
        }
    }
}