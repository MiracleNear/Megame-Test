using System;
using DefaultNamespace.Menu;
using UnityEngine;


[RequireComponent(typeof(PlayerLifeView))]
public class PlayerLife : MonoBehaviour
{
    public int Amount => _amount;
    
    private PlayerLifeView _playerLifeView;
    private int _amount = 3;
        
    private void Awake()
    { 
        _playerLifeView = GetComponent<PlayerLifeView>();
    }

    private void Start()
    {
        _playerLifeView.Display(_amount);
    }
    
    public void DecreaseByOne()
    {
        _amount -= 1;

        _playerLifeView.Display(_amount);
    }
    
}
