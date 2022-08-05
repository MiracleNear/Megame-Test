using Spawner;
using UnityEngine;

namespace Factories
{
    public abstract class EnemyPlacerFactory : MonoBehaviour
    {
        public abstract IEnemyPlacer Get();
    }
}