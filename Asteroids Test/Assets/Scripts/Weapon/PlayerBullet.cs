using System;
using Enemies;
using UnityEngine;

namespace DefaultNamespace.Weapon
{
    public class PlayerBullet : Bullet
    {
        private Action<int> _enemyHit;

        public void Init(Action<int> EnemyHit)
        {
            _enemyHit = EnemyHit;
        }
        
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                _enemyHit?.Invoke(enemy.Points);
            }
        }
    }
}