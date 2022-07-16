using System;
using DefaultNamespace.Audio;
using DefaultNamespace.GameSession;
using Factories;
using Handlers;
using UnityEngine;

[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(Invulnerability))]
[RequireComponent(typeof(GameZoneOutBoundsHandler))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour, IAsteroidCollisionHandler, IBulletCollisionHandler
{
    public event Action Died;
    
    [SerializeField] private ExplosionSFX _explosionSfx;
    [SerializeField] private AudioClip _soundDeath;
    [SerializeField] private float _rotatePerSecond;
    [SerializeField] private float _speedPerSecond;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private AudioSource _acclerationSoundSource;
    
    private Vector3 _acceleration;
    private PlayerWeapon _playerWeapon;
    private Invulnerability _invulnerability;

    private void Awake()
    {
        _playerWeapon = GetComponent<PlayerWeapon>();
        _invulnerability = GetComponent<Invulnerability>();
    }

    private void Start()
    {
        _invulnerability.Activate();
    }


    public void OnCollisionAsteroid()
    {
        Destroy();
    }

    public void OnCollisionBullet(Bullet bullet, Action onCollisionSuccessful)
    {
        if(bullet.Type == BulletType.Ufo)
        {
            onCollisionSuccessful?.Invoke();

            Destroy();
        }
    }
    
    public void Accelerate()
    {
        _acceleration = _acceleration + transform.up * (_speedPerSecond * Time.deltaTime);

        _acceleration = Vector2.ClampMagnitude(_acceleration, _maxSpeed);

        if (!_acclerationSoundSource.isPlaying && !SceneContext.Instance.PauseManager.IsPaused)
        {
            _acclerationSoundSource.Play();
        }
    }

    public void Shoot()
    {
        if (_playerWeapon.CanShoot())
        {
            _playerWeapon.Shoot(transform.up);
        }
    }
    
    public void Move()
    {
        transform.position += _acceleration * Time.deltaTime;
    }

    public void RotateAt(float direction)
    {
        transform.Rotate(Vector3.forward, _rotatePerSecond * Time.deltaTime * direction, Space.World);
    }

    public void LookAt(Vector2 position)
    {
        Vector2 directionLook = (position - (Vector2)transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, directionLook);
            
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotatePerSecond * Time.deltaTime);
    }

    private void Destroy()
    {
        if(_invulnerability.IsActivated) return;

        Instantiate(_explosionSfx).Init(_soundDeath);
        
        Died?.Invoke();
        
        Destroy(gameObject);
    }
}
