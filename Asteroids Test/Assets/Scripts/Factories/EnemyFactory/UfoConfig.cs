using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(fileName = "Ufo Config", menuName = "Configs/Ufo Config", order = 0)]
    public class UfoConfig : EnemyConfig
    {
        public Sprite[] Sprites;
    }
}