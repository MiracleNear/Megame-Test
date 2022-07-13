using UnityEngine;

public class UfoWeapon : Weapon
{
    [SerializeField] private float _minimumRechargeTime, _maximumRechargeTime;
    
    public override void Shoot(Vector2 direction)
    {
        float rechargeTime = Random.Range(_maximumRechargeTime, _maximumRechargeTime);
        
        UpdateIntervalBetweenShot(rechargeTime);
        
        base.Shoot(direction);
    }
}
