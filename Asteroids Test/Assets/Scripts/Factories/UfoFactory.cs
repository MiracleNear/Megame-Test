using System;
using Enemies;
using UnityEngine;

namespace Factories
{
    public class UfoFactory : EnemyFactory
    {
        [SerializeField] private EnemyConfig _ufoConfig;
        [SerializeField] private Ufo _template;
        public override void Reclaim(Enemy enemy)
        {
            Ufo ufo = enemy as Ufo;

            if(ufo != null)
            {
                Destroy(ufo);
            }
        }

        protected override Enemy GetInstance()
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