using System;
using Enemies;
using ObjectPools;
using UnityEngine;

namespace Factories
{
    [RequireComponent(typeof(AsteroidPool))]
    public class AsteroidFactory : EnemyFactory
    {
        private IObjectPool<Asteroid> _asteroidsPool;

		private void Awake()
        {
            _asteroidsPool = GetComponent<AsteroidPool>();
        }

        protected override Enemy GetInstance()
        {
            return _asteroidsPool.GetFreeElement();
        }

		public override void Reclaim(Enemy enemy)
		{
            _asteroidsPool.ReturnToPool(enemy as Asteroid);
		}
	}
}