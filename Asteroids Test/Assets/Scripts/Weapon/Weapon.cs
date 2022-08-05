using DefaultNamespace.GameSession;
using Factories;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Color _bulletCollor;
    [SerializeField] private AudioClip _shotSound;
    
    private IBulletFactory _bulletFactory;
    private float _timePreviousShot;
    private float _shotWatingTime;
    private AudioSource _audioSource;

    private void Awake()
    {
        

        _audioSource = GetComponent<AudioSource>();
        
        Init();
    }

    public virtual void Shoot(Vector2 direction)
    {
        _bulletFactory.Create(_shotPoint.position, direction, _bulletCollor);
        
        _audioSource.PlayOneShot(_shotSound);
    }

    public bool CanShoot()
    {
        if (SceneContext.Instance.PauseManager.IsPaused)
        {
            return false;
        }

        if (Time.time - _timePreviousShot <= _shotWatingTime)
        {
            return false;
        }

        return true;
    }

    protected virtual void Init()
    {
        
    }

    protected void UpdateIntervalBetweenShot(float intervalBetweenShot)
    {
        _timePreviousShot = Time.time;
        _shotWatingTime = intervalBetweenShot;
    }
}
