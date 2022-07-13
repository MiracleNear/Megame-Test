using System;
using System.Collections;
using DefaultNamespace.Menu;
using Handlers;
using UnityEngine;

[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(Invulnerability))]
[RequireComponent(typeof(GameZoneOutBoundsHandler))]
public class Player : MonoBehaviour, IAsteroidCollisionHandler, IBulletCollisionHandler
{
    public event Action<Player> Died;
    
    [SerializeField] private float _rotatePerSecond;
    [SerializeField] private float _speedPerSecond;
    [SerializeField] private float _maxSpeed;
    
    private Vector2 _acceleration;
    private PlayerWeapon _playerWeapon;
    private Invulnerability _invulnerability;
    private KeyboardInput _keyboardInput;
    private PlayerLife _playerLife;

    private void Awake()
    {
        _playerWeapon = GetComponent<PlayerWeapon>();
        _invulnerability = GetComponent<Invulnerability>();
    }

    private void Start()
    {
        _invulnerability.Activate();
    }

    public void Init(PlayerLife playerLife)
    {
        _playerLife = playerLife;
    }
    
    private void Update()
    {
        if (_keyboardInput.IsAccelerateButtonPressed)
        {
            Accelerate(transform.up);
        }

        if (_keyboardInput.IsShotButtonPressed && _playerWeapon.CanShoot())
        {
            _playerWeapon.Shoot(transform.up);
        }
        
        Move(_acceleration);
        RotateAt(_keyboardInput.InverseDirectionRotation);
    }

    public void OnCollisionAsteroid()
    {
        Destroy();
    }

    public void OnCollisionBullet()
    {
        Destroy();
    }
    
    
    public void BindInput(KeyboardInput input)
    {
        _keyboardInput = input;
    }
    
    private void Accelerate(Vector2 direction)
    {
        _acceleration = _acceleration + direction * (_speedPerSecond * Time.deltaTime);

        _acceleration = Vector2.ClampMagnitude(_acceleration, _maxSpeed);
    }


    private void Move(Vector2 delta)
    {
        transform.position += (Vector3) delta * Time.deltaTime;
    }

    private void RotateAt(float direction)
    {
        transform.Rotate(Vector3.forward, _rotatePerSecond * Time.deltaTime * direction, Space.World);
    }


    private void Destroy()
    {
        if(_invulnerability.IsActivated) return;
        
        _playerLife.DecreaseByOne();
        
        Died?.Invoke(this);
        
        Destroy(gameObject);
    }
}
