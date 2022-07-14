using UnityEngine;
using Enemies;
using System;


public class EnemyPublisher : MonoBehaviour
{
	public event Action<Enemy> Spawned;
	
	public void Publish(Enemy enemy)
	{
		Spawned?.Invoke(enemy);
	}
}
