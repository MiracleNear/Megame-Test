using System;
using CollisionInterface;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private Action<int> _enemyHit;

    public void Init(Action<int> EnemyHit)
    {
        _enemyHit = EnemyHit;
    }

    protected override bool TryCollisionWith(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out IPlayerBulletCollisionHandler bulletCollisionHandler))
        {
            bulletCollisionHandler.OnCollisionPlayerBullet(_enemyHit);
            return true;
        }

        return false;
    }
        
}
