using UnityEngine;

namespace Spawner
{
    public class SpawnInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private UfoSpawner _ufoSpawner;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;

        public void Init()
        {
            
        }
    }
}