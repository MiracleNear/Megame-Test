using Factories;
using UnityEngine;
using WeaponSystem;

namespace Enemies
{
    [RequireComponent(typeof(UfoWeapon))]
    public class Ufo : Enemy
    {
        private Transform _target;
        private UfoWeapon _ufoWeapon;

        private void Awake()
        {
            _ufoWeapon = GetComponent<UfoWeapon>();
        }
        public void Init(UfoConfig enemyConfig)
        {
            Sprite sprite = enemyConfig.Sprites[Random.Range(0, enemyConfig.Sprites.Length)];
            
            Init(enemyConfig, sprite);
        }
        
        public void SetTarget(Transform target)
        {
            _target = target;
        }
        
        public void Kill()
        {
            DestroySelf();
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerBullet playerBullet))
            {
                DestroySelf();
            }
        }

        protected override void Shot()
        {
            if (_target != null && _ufoWeapon.CanShoot())
            {
                _ufoWeapon.Shoot((_target.position - transform.position).normalized);
            }
        }
    }
}