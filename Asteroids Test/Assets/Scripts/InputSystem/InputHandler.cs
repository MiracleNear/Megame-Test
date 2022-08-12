using GameSession;
using Spawner;
using UnityEngine;

public class InputHandler : MonoBehaviour, IGameStartListener
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    
    private Player _player;
    private IInput _input;
    private bool _isCanHandleInput;
    private bool _isPlayerNotNull => _player != null;
    private bool _isPaused => PauseManager.GetInstance().IsPaused;
    
    private void Awake()
    {
        EventBus.Subscribe<IGameStartListener>(this);
    }

    private void OnEnable()
    {
        _playerSpawner.Spawned += BindPlayer;
    }

    private void OnDisable()
    {
        _playerSpawner.Spawned -= BindPlayer;
    }

    private void OnDestroy()
    {
        EventBus.UnSubscribe<IGameStartListener>(this);
    }

    public void OnStartGame()
    {
        _isCanHandleInput = true;
    }
    

    public void SetInput(IInput input)
    {
        _input = input;
    }

    private void BindPlayer(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        if(!_isCanHandleInput) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventBus.Send<IPauseButtonClickListener>(listener => listener.OnClickPauseButton());
        }

        if (_isPlayerNotNull && !_isPaused)
        {
            _input.Update(_player);

            _player.Move();
        }
    }
}