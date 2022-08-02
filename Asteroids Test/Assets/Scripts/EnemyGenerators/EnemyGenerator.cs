using System;
using Enemies;
using Factories;
using Spawner;
using UnityEngine;

public abstract class EnemyGenerator<T> : MonoBehaviour where T : Enemy
{
    [SerializeField] private EnemyPublisher _enemyPublisher;
    [SerializeField] private EnemyFactory _enemyFactory;
    
    protected EnemySpawner EnemySpawner;
    
    private Action<T> _generated;
    private Action<T> _removed;

    public virtual void Init(Action<T> generated, Action<T> removed)
    {
        _generated = generated;
        _removed = removed;
    }

    public abstract void Generate(EnemyType type, int count);

    protected T GetEnemy(EnemyType enemyType, EnemySpawner enemySpawner)
    {
        T enemy = _enemyFactory.Get(enemyType) as T;
        enemySpawner.Spawn(enemy);
        SendGeneratedEnemy(enemy);
        
        return enemy;
    }

    protected virtual void Removed(T enemy)
    {
        _removed?.Invoke(enemy);
    }

    private void SendGeneratedEnemy(T enemy)
    {
        _generated?.Invoke(enemy);
        SubScribe(enemy);
    }

    private void OnDied(Enemy enemy)
    {
        UnSubscribe(enemy);
        Removed(enemy as T);
    }

    private void SubScribe(Enemy enemy)
    {
        enemy.Died += OnDied;
        _enemyPublisher.Publish(enemy);
    }

    private void UnSubscribe(Enemy enemy)
    {
        enemy.Died -= OnDied;
        _enemyPublisher.Remove(enemy);
        _enemyFactory.Reclaim(enemy);
    }
}
