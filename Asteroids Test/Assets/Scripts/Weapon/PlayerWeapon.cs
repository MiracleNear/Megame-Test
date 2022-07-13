using UnityEngine;

public class PlayerWeapon : Weapon
{
    [SerializeField] private int _numberOfBulletsPerSecond;

    private float _intervalBetweenShot;

    protected override void Init()
    {
        _intervalBetweenShot = (1f / _numberOfBulletsPerSecond);
    }

    public override void Shoot(Vector2 direction)
    {
        UpdateIntervalBetweenShot(_intervalBetweenShot);
        
        base.Shoot(direction);
    }
}