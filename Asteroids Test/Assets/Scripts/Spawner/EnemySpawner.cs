using System;
using Enemies;
using Factories;
using UnityEngine;

namespace Spawner
{
    public abstract class EnemySpawner : MonoBehaviour
    {
	    [SerializeField] private EnemyPublisher _enemyPublisher;

	    public abstract void Init(Transform transform);

	    public abstract void Dispose();

	    protected void Add(Enemy enemy)
		{
           _enemyPublisher.Publish(enemy);
		}

	    protected  void Revoke(Enemy enemy)
        {
	        _enemyPublisher.Remove(enemy);
        }
    }
}