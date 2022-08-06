using System;
using CollisionInterface;
using DefaultNamespace.Audio;
using Detectors;
using Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    [RequireComponent(typeof(GameZoneOutBoundsDetector))]
    public abstract class Enemy : MonoBehaviour, IPlayerBulletCollisionHandler
    {
        public event Action<Enemy> Died;
        public EnemyType Type { get; private set; }
        public Vector3 Direction { get; set; }

        [SerializeField] private ExplosionSFX _explosionSfx;
        
        private float _speed;
        private AudioClip _deathSound;
        private int _points;

        public void Init(EnemyConfig enemyConfig, EnemyType type)
        {
            Type = type;
            _speed = Random.Range(enemyConfig.MinSpeed, enemyConfig.MaxSpeed);
            _deathSound = enemyConfig.DeathSound;
            transform.localScale = enemyConfig.Scale;
            _points = enemyConfig.Points;
        }

        public virtual void OnCollisionPlayerBullet(Action<int> action)
        {
            action.Invoke(_points);
            DestroySelf();
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

        private void PlaySoundDeath()
        {
            Instantiate(_explosionSfx).Init(_deathSound);
        }
    }
}