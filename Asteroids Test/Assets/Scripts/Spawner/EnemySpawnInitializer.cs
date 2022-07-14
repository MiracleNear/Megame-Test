using Spawner;
using UnityEngine;


public class EnemySpawnInitializer : MonoBehaviour
{
	[SerializeField] private EnemySpawner[] _enemySpawners;

	public void Init(Player player)
	{	
		foreach(EnemySpawner enemySpawner in _enemySpawners)
		{
			enemySpawner.Init(player.transform);
		}
	}

	public void Dispose()
	{
		foreach(EnemySpawner enemySpawner in _enemySpawners)
		{
			enemySpawner.Dispose();
		}
	}
}