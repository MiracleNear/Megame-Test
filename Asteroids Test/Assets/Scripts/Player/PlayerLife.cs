using UnityEngine;

[RequireComponent(typeof(PlayerLifeView))]
public class PlayerLife : MonoBehaviour
{
    public int Amount => _amount;

    [SerializeField] private GameStarter _gameStarter;
    
    private PlayerLifeView _playerLifeView;
    private int _amount = 3;
        
    private void Awake()
    {
        _playerLifeView = GetComponent<PlayerLifeView>();
        _gameStarter.GameLaunched += OnGameLaunched;
        
        gameObject.SetActive(false);
    }
    
    public void DecreaseByOne()
    {
        _amount -= 1;

        _playerLifeView.Display(_amount);
    }

    private void OnGameLaunched()
    {
        gameObject.SetActive(true);
        
        _playerLifeView.Display(_amount);
    }
}
