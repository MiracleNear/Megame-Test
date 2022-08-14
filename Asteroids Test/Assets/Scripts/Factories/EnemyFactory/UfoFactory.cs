using System;
using Enemies;
using UnityEngine;

namespace Factories
{
    public class UfoFactory : EnemyFactory<Ufo>
    {
        [SerializeField] private UfoConfig _ufoConfig;
        [SerializeField] private Ufo _template;

        protected override void Reclaim(Ufo element)
        {
            base.Reclaim(element);
            Destroy(element.gameObject);
        }
        

        protected override Ufo GetInstance(EnemyType enemyType)
        {
            UfoConfig ufoConfig = GetConfigByType(enemyType);
            
            Ufo ufo = Instantiate(_template);
            
            ufo.Init(ufoConfig);

            return ufo;
        }

        private UfoConfig GetConfigByType(EnemyType enemyType)
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