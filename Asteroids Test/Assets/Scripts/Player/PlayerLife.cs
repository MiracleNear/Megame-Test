using UnityEngine;

[RequireComponent(typeof(PlayerLifeView))]
public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int _amount;

    private PlayerLifeView _playerLifeView;

    private void Awake()
    {
        _playerLifeView = GetComponent<PlayerLifeView>();

        gameObject.SetActive(false);
    }

    public bool TryDecrease()
    {
        _amount -= 1;

        if (_amount >= 0)
        {
            _playerLifeView.Display(_amount);
            return true;
        }

        return false;
    }

    private void OnGameLaunched()
    {
        gameObject.SetActive(true);
        
        _playerLifeView.Display(_amount);
    }
}
