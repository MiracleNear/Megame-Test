using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(UfoWeapon))]
    public class Ufo : Enemy, IAsteroidCollisionHandler
    {
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
            DestroySelf();
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