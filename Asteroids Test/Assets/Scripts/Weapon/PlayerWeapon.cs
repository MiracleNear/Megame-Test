using UnityEngine;

public class PlayerWeapon : Weapon
{
    [SerializeField] private int _numberOfBulletsPerSecond;

    private float _intervalBetweenShot;

    public override void Shoot(Vector2 direction)
    {
        UpdateIntervalBetweenShot(_intervalBetweenShot);
        
        base.Shoot(direction);
    }

    protected override void Init()
    {
        _intervalBetweenShot = (1f / _numberOfBulletsPerSecond);
    }
}