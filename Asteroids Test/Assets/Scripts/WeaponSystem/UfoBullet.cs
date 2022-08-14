using Enemies;
using UnityEngine;

namespace WeaponSystem
{
    public class UfoBullet : Bullet
    {
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player) ||
                other.gameObject.TryGetComponent(out Asteroid asteroid))
            {
                Destroy();
            }
        }
    }
}
