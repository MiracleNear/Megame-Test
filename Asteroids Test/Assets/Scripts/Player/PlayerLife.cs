using Menu;
using UnityEngine;

[RequireComponent(typeof(PlayerLifeView))]
public class PlayerLife : UserInterfaceElement
{
    [SerializeField] private int _amount;

    private PlayerLifeView _playerLifeView;
    
    public override void Init()
    {
        _playerLifeView = GetComponent<PlayerLifeView>();
    }

    public override void OnStartGame()
    {
        _playerLifeView.Display(_amount);
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
}
