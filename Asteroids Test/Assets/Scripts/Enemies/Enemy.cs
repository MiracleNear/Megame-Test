using System;
using DefaultNamespace.Audio;
using Detectors;
using Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    [RequireComponent(typeof(GameZoneOutBoundsDetector))]
    public abstract class Enemy : MonoBehaviour, IBulletCollisionHandler
    {
        public event Action<int> DestroyedByPlayer;
        public int Points { get; private set; }
        public Vector3 Direction { get; private set; }

        [SerializeField] private ExplosionSFX _explosionSfx;
        
        private float _speed;
        private AudioClip _deathSound;
        
        public void Init(EnemyConfig enemyConfig, Vector2 position, Vector2 direction)
        { 
            Direction = direction;
            _speed = Random.Range(enemyConfig.MinSpeed, enemyConfig.MaxSpeed);
            _deathSound = enemyConfig.DeathSound;
            
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

        protected void Move()
        {
            transform.position = transform.position + Direction * (_speed * Time.deltaTime);
        }

        protected void PlaySoundDeath()
        {
            Instantiate(_explosionSfx).Init(_deathSound);
        }
        

        private void DestroyByPlayer()
		{
            DestroyedByPlayer?.Invoke(Points);
		}
    }
}