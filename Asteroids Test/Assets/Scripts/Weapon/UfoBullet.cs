using UnityEngine;

namespace DefaultNamespace.Weapon
{
    public class UfoBullet : Bullet
    {
        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                
            }
        }
    }
}