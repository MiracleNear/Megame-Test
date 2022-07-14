using System;
public interface IBulletCollisionHandler
{
    public void OnCollisionBullet(Bullet bullet, Action onCollisionSuccessful);
}
