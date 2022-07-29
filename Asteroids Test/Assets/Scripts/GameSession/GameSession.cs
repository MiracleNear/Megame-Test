using DefaultNamespace.GameSession;
using Spawner;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerSpawner), typeof(EnemySpawnInitializer))]
public class GameSession : MonoBehaviour
{ 	
	[SerializeField] private PlayerLife _playerLife;
	[SerializeField] private InputHandler _inputHandler;
	[SerializeField] private GameStarter _gameStarter;
	[SerializeField] private Button _exitGameButton;
	
	private PlayerSpawner _playerSpawner;
    private EnemySpawnInitializer _enemySpawnerInitializer;

    private void Awake()
	{
		_playerSpawner = GetComponent<PlayerSpawner>();
		_enemySpawnerInitializer = GetComponent<EnemySpawnInitializer>();
	}

	private void OnEnable()
	{
		_gameStarter.GameLaunched += OnGameLaunched;
		_exitGameButton.onClick.AddListener(OnClickButtonExitGame);
	}

	private void OnDisable()
	{
		_gameStarter.GameLaunched -= OnGameLaunched;
		_exitGameButton.onClick.RemoveListener(OnClickButtonExitGame);
	}

	private void OnGameLaunched()
	{
		_playerSpawner.Init(OnSpawned);

		_playerSpawner.InitialSpawn();
	}
	
	private void OnSpawned(Player player)
	{	
		_inputHandler.BindPlayer(player);
		
		player.Died += OnDied;
	}

	private void OnDied()
	{
		_playerLife.DecreaseByOne();

		if(_playerLife.Amount < 0)
		{
			SceneContext.Instance.GameOver();
		}
		else
		{
			_playerSpawner.SpawnPlayerWithDelay();
		}
	}

	private void OnClickButtonExitGame()
	{
		Application.Quit();
	}
	
}
