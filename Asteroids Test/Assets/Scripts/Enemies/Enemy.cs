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
        public event Action<Enemy> DestroyedByPlayer;

        public event Action<Enemy> Died;
        public EnemyType Type { get; private set; }
        public int Points { get; private set; }
        public Vector3 Direction { get; set; }

        [SerializeField] private ExplosionSFX _explosionSfx;
        
        private float _speed;
        private AudioClip _deathSound;
        
        public void Init(EnemyConfig enemyConfig, EnemyType type)
        {
            Type = type;
            _speed = Random.Range(enemyConfig.MinSpeed, enemyConfig.MaxSpeed);
            _deathSound = enemyConfig.DeathSound;
            transform.localScale = enemyConfig.Scale;
            Points = enemyConfig.Points;
        }

        public void OnCollisionBullet(Bullet bullet, Action onCollisionSuccessful)
		{
            if(bullet.Type == BulletType.Player)
			{
                DestroyedByPlayer?.Invoke(this);

                DestroySelf();
			}
		}


        protected void Move()
        {
            transform.position = transform.position + Direction * (_speed * Time.deltaTime);
        }


        protected void DestroySelf()
		{
            Died?.Invoke(this);
            PlaySoundDeath();
		}

        protected void PlaySoundDeath()
        {
            Instantiate(_explosionSfx).Init(_deathSound);
        }
        
    }
}