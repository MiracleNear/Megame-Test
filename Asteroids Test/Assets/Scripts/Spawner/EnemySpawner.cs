using System;
using Enemies;
using Factories;
using UnityEngine;

namespace Spawner
{
    public abstract class EnemySpawner : MonoBehaviour
    {
        public void Add(Enemy enemy)
		{
           
		}

        public abstract void Init(Transform transform);

        public abstract void Dispose();

	}
}