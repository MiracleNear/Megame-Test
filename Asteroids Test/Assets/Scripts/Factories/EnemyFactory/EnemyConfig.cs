using UnityEngine;

namespace Factories
{
    public abstract class EnemyConfig : ScriptableObject
    {
        public int Points;
        public float MinSpeed, MaxSpeed;
        public AudioClip DeathSound;
    }
}