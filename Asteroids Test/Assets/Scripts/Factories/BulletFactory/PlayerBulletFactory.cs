using DefaultNamespace.Score;
using DefaultNamespace.Weapon;
using ObjectPools;
using UnityEngine;

namespace Factories
{
    [RequireComponent(typeof(PlayerBulletPool))]
    public class PlayerBulletFactory : BulletFactory<PlayerBullet>
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        
        protected override void InitFactory()
        {
            _bulletPool = GetComponent<PlayerBulletPool>();
        }

        protected override PlayerBullet Init(PlayerBullet bullet)
        {
            bullet.Init(_scoreCounter.Increase);

            return bullet;
        }
    }
}