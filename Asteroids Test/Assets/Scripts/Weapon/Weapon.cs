using Factories;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Color _bulletColor;
    [SerializeField] private string _layerMaskName;
    
    private BulletFactory _bulletFactory;
    private float _timePreviousShot;
    private float _shotWatingTime;
    
    private void Awake()
    {
        _bulletFactory = FindObjectOfType<BulletFactory>();
        
        Init();
    }

    protected virtual void Init()
    {
        
    }

    protected void UpdateIntervalBetweenShot(float intervalBetweenShot)
    {
        _timePreviousShot = Time.time;
        _shotWatingTime = intervalBetweenShot;
    }
    
    public bool CanShoot()
    {
        if (Time.time - _timePreviousShot >= _shotWatingTime)
        {
            return true;
        }

        return false;
    }

    public virtual void Shoot(Vector2 direction)
    {
        _bulletFactory.Create(_shotPoint.position, direction, _layerMaskName, _bulletColor);
    }
}
