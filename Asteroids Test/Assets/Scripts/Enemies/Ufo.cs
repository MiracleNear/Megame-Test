using Factories;
using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(UfoWeapon))]
    public class Ufo : Enemy, IAsteroidCollisionHandler
    {
        public event Action Died;

        private Transform _target;
        private UfoWeapon _ufoWeapon;

        private void Awake()
        {
            _ufoWeapon = GetComponent<UfoWeapon>();
        }

        public void Init(Transform target)
        {
            _target = target;
        }
        
        public void OnCollisionAsteroid()
        {
            Died?.Invoke();
        }

		public override void OnCollisionBullet(Bullet bullet, Action onCollisionSuccessful)
		{
            if(IsDestroyByPlayer(bullet))
			{
                Died?.Invoke();

                onCollisionSuccessful?.Invoke();
			}
		}

	
        private void Update()
        {
            Move();

            if (_ufoWeapon.CanShoot())
            {
                _ufoWeapon.Shoot((_target.position - transform.position).normalized);
            }
        }

	}
}