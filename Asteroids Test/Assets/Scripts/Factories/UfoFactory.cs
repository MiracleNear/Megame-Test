using Enemies;
using UnityEngine;

namespace Factories
{
    public class UfoFactory : EnemyFactory<Ufo>
    {
        [SerializeField] private EnemyConfig _ufoConfig;
        [SerializeField] private Ufo _template;

        public Ufo Create(Vector2 position, Vector2 direction, Transform target)
        {
            Ufo ufo = Instantiate(_template);
            
            ufo.Init(target);
            
            return Create(ufo, _ufoConfig, position, direction);
        }

        public override void Reclaim(Ufo enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}