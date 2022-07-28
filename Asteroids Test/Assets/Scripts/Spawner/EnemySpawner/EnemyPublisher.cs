using UnityEngine;
using Enemies;
using System;

public class EnemyPublisher : MonoBehaviour
{
	public event Action<Enemy> Spawned;
	public event Action<Enemy> Removed; 
	
	public void Publish(Enemy enemy)
	{
		Spawned?.Invoke(enemy);
	}

	public void Remove(Enemy enemy)
	{
		Removed?.Invoke(enemy);
	}
}
