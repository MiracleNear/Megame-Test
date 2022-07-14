using Spawner;
using UnityEngine;

[RequireComponent(typeof(PlayerSpawner), typeof(EnemySpawnInitializer))]
public class GameSession : MonoBehaviour
{ 	
	[SerializeField] private PlayerLife _playerLife;

	private PlayerSpawner _playerSpawner;
    private EnemySpawnInitializer _enemySpawnerInitializer;

	private void Awake()
	{
		_playerSpawner = GetComponent<PlayerSpawner>();
		_enemySpawnerInitializer = GetComponent<EnemySpawnInitializer>();
	}

	private void Start()
	{
		_playerSpawner.Init(OnSpawned);

		_playerSpawner.InitialSpawn();
	}


	private void OnSpawned(Player player)
	{
		_enemySpawnerInitializer.Init(player);

		player.Died += OnDied;
	}

	private void OnDied()
	{
		_enemySpawnerInitializer.Dispose();

		_playerLife.DecreaseByOne();

		if(_playerLife.Amount < 0)
		{
			Debug.Log("Lose");
		}
		else
		{
			_playerSpawner.SpawnPlayerWithDelay();
		}
	}
}
