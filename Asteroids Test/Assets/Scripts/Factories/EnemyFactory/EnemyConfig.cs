using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(fileName = "Enemy Config", menuName = "Configs/Enemy Config", order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        public int Points;
        public Vector2 Scale;
        public float MinSpeed, MaxSpeed;
        public AudioClip DeathSound;
    }
}