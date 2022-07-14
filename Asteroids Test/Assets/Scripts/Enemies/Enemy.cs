using System;
using Factories;
using Handlers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    [RequireComponent(typeof(GameZoneOutBoundsHandler))]
    public abstract class Enemy : MonoBehaviour, IBulletCollisionHandler
    {
        public event Action<int> DestroyedByPlayer;
        public int Points { get; private set; }
        
        public Vector3 Direction { get; private set; }
        
        private float _speed;

        public void Init(EnemyConfig enemyConfig, Vector2 position, Vector2 direction)
        { 
            Direction = direction;
            _speed = Random.Range(enemyConfig.MinSpeed, enemyConfig.MaxSpeed);
            
            transform.position = position;
            transform.localScale = enemyConfig.Scale;
            Points = enemyConfig.Points;
        }

        public abstract void OnCollisionBullet(Bullet bullet, Action onCollisionSuccessful);

        protected bool IsDestroyByPlayer(Bullet bullet)
		{
            if(BulletType.Player == bullet.Type)
			{
                DestroyByPlayer();

                return true;
			}

            return false; 
		}            

        private void DestroyByPlayer()
		{
            DestroyedByPlayer?.Invoke(Points);
		}

        protected void Move()
        {
            transform.position = transform.position + Direction * (_speed * Time.deltaTime);
        }

    }
}