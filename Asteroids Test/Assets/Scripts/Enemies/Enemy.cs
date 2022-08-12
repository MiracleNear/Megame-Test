using System;
using CollisionInterface;
using DefaultNamespace.Audio;
using Detectors;
using Factories;
using GameSession;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    [RequireComponent(typeof(GameZoneOutBoundsDetector))]
    public abstract class Enemy : MonoBehaviour, IPlayerBulletCollisionHandler
    {
        public IFactoryGameElements FactoryGameElements;
        public Vector3 Direction { get; set; }

        [SerializeField] private ExplosionSFX _explosionSfx;

        private bool _isPaused => PauseManager.GetInstance().IsPaused;
        private float _speed;
        private AudioClip _deathSound;
        private int _points;
        
        private void Update()
        {
            if(_isPaused) return;
            
            Move();
            Shot();
        }

        public void Init(EnemyConfig enemyConfig)
        {
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
        

        protected void DestroySelf()
		{
            PlaySoundDeath();
            FactoryGameElements.Recycle(this);
		}

        protected virtual void Shot()
        {
            
        }

        private void PlaySoundDeath()
        {
            Instantiate(_explosionSfx).Init(_deathSound);
        }

        private void Move()
        {
            transform.position = transform.position + Direction * (_speed * Time.deltaTime);
        }
    }
}