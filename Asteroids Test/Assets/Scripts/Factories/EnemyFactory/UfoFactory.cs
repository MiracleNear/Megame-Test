using System;
using Enemies;
using UnityEngine;

namespace Factories
{
    public class UfoFactory : EnemyFactory<Ufo>
    {
        [SerializeField] private EnemyConfig _ufoConfig;
        [SerializeField] private Ufo _template;

        protected override void Reclaim(Ufo element)
        {
            base.Reclaim(element);
            Destroy(element.gameObject);
        }
        

        protected override Ufo GetInstance(EnemyType enemyType)
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