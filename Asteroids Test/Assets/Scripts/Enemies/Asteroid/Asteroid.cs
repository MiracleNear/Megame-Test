using Factories;
using UnityEngine;
using WeaponSystem;

namespace Enemies
{
    public class Asteroid : Enemy
    {
        [SerializeField] private BoxCollider2D _boxCollider2D;
        
        private AsteroidType _asteroidType;
        
        public void Init(AsteroidConfig enemyConfig, AsteroidType asteroidType)
        {
            _asteroidType = asteroidType;

            AsteroidSetting asteroidSetting = enemyConfig.AsteroidSettings[Random.Range(0, enemyConfig.AsteroidSettings.Length)];

            _boxCollider2D.size = asteroidSetting.BoxCollider2D.size;
            _boxCollider2D.offset = asteroidSetting.BoxCollider2D.offset;

            transform.localScale = enemyConfig.Scale;
            
            Init(enemyConfig, asteroidSetting.Sprite);
        }
        
        protected override void OnCollisionEnter2D(Collision2D other)
        { 
           if(other.gameObject.TryGetComponent(out Bullet bullet))
           {
               CreatePartAsteroids();
               DestroySelf();
           }
           else if (other.gameObject.TryGetComponent(out Player player))
           {
               DestroySelf();
           }
        }


        private void CreatePartAsteroids()
        {
            _asteroidType.CreatePartAsteroidsFor(this);
        }
    }
}