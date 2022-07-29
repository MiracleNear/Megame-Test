using Enemies;
using UnityEngine;

namespace Factories
{
    public class UfoFactory : EnemyFactory
    {
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
	}
}