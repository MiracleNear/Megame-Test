using System;
using Enemies;
using UnityEngine;

namespace Factories
{
    public class UfoFactory : EnemyFactory<Ufo>
    {
        [SerializeField] private EnemyConfig _ufoConfig;
        [SerializeField] private Ufo _template;
        public override void Reclaim(Ufo enemy)
        {
            Destroy(enemy.gameObject);
        }

        protected override Ufo GetInstance()
        {
            return Instantiate(_template);
        }

        protected override EnemyConfig GetConfigByType(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Ufo:
                    return _ufoConfig;
                default:
                    throw new Exception("no such template");
            }
        }
    }
}