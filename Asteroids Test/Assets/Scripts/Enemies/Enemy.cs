using DefaultNamespace.Audio;
using Detectors;
using Factories;
using GameSession;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    [RequireComponent(typeof(GameZoneOutBoundsDetector))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Enemy : MonoBehaviour
    {
        public IFactoryGameElements FactoryGameElements;
        public Vector3 Direction { get; set; }
        public int Points { get; private set; }

        [SerializeField] private ExplosionSFX _explosionSfx;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private bool _isPaused => PauseManager.GetInstance().IsPaused;
        private float _speed;
        private AudioClip _deathSound;
        
        private void Update()
        {
            if(_isPaused) return;
            
            Move();
            Shot();
        }

        protected void Init(EnemyConfig enemyConfig, Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            _speed = Random.Range(enemyConfig.MinSpeed, enemyConfig.MaxSpeed);
            _deathSound = enemyConfig.DeathSound;
            Points = enemyConfig.Points;
        }

        protected abstract void OnCollisionEnter2D(Collision2D other);

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