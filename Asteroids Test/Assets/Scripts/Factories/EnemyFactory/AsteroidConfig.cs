using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(fileName = "Asteroid Config", menuName = "Configs/Asteroid Config", order = 0)]
    public class AsteroidConfig : EnemyConfig
    {
        public AsteroidSetting[] AsteroidSettings;
        public Vector2 Scale;
    }

    [System.Serializable]
    public class AsteroidSetting
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public Sprite Sprite => _spriteRenderer.sprite;
        
        public BoxCollider2D BoxCollider2D;
    }
}