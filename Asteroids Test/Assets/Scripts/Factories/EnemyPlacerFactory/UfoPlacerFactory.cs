using Spawner;
using UnityEngine;

namespace Factories
{
    public class UfoPlacerFactory : EnemyPlacerFactory
    {
        [SerializeField] private float _offsetFromTheVerticalBorderInPercent;
        [SerializeField] private Collider2D _ufoCollider;

        public override IEnemyPlacer Get()
        {
            UfoPlacer ufoPlacer = new UfoPlacer(_ufoCollider, _offsetFromTheVerticalBorderInPercent);

            return ufoPlacer;
        }
    }
}