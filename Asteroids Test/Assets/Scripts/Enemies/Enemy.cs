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

        public event Action<Enemy> Destroyed; 
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

        public virtual void OnCollisionBullet()
        {
            DestroyedByPlayer?.Invoke(Points);
            DestroySelf();
            
        }

        protected void Move()
        {
            transform.position = transform.position + Direction * (_speed * Time.deltaTime);
        }

        protected void DestroySelf()
        {
            Destroyed?.Invoke(this);
        }
    }
}