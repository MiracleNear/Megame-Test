using System;
using Enemies;
using Factories;
using Spawner;
using UnityEngine;

public class EnemyGenerator<T> : MonoBehaviour where T : Enemy
{
    [SerializeField] private EnemyFactory<T> _enemyFactory;
    [SerializeField] private EnemyPublisher _enemyPublisher;

    private Action<T> _removed;
    
    public void Init(Action<T> removed)
    {
        _removed = removed;
    }
    
    public T Create(EnemyType enemyType, IEnemyPlacer enemyPlacer)
    {
        var enemy =  _enemyFactory.Get(enemyType);
        
        PlaceEnemy(enemy, enemyPlacer);
    
        SendEnemy(enemy);
        
        return enemy;
    }

    private void PlaceEnemy(T enemy, IEnemyPlacer enemyPlacer)
    {
        Vector2 position = enemyPlacer.GetPosition();
        Vector2 direction = enemyPlacer.GetDirectionFrom(position);

        enemy.transform.position = position;
        enemy.Direction = direction;
    }
    
    private void SendEnemy(T enemy)
    {
        enemy.Died += OnDied;
        _enemyPublisher.Publish(enemy);
    }

    private void OnDied(Enemy enemy)
    {
       DestroyEnemy(enemy as T);
    }

    private void DestroyEnemy(T enemy)
    {
        enemy.Died -= OnDied;
        _enemyPublisher.Remove(enemy);
        _removed.Invoke(enemy);
        _enemyFactory.Reclaim(enemy);
    }
}
