using Factories;
using UnityEngine;

namespace WeaponSystem
{
    public class UfoWeapon : Weapon
    {
        [SerializeField] private float _minimumRechargeTime, _maximumRechargeTime;

        protected override void Init()
        {
            BulletFactory = FindObjectOfType<UfoBulletFactory>();
        }

        public override void Shoot(Vector2 direction)
        {
            float rechargeTime = Random.Range(_maximumRechargeTime, _maximumRechargeTime);
        
            UpdateIntervalBetweenShot(rechargeTime);
        
            base.Shoot(direction);
        }
    }
}
