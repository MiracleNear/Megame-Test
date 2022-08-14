using Enemies;
using System;
using Factories;
using UnityEngine;

namespace Factories
{
    public enum EnemyType
	{
        LargeAsteroid,
        MediumAsteroid,
        SmallAsteroid,
        Ufo,
	}

    public abstract class EnemyFactory<T> : FactoryGameElements<T> where T : Enemy
    {
	    public event Action<T> Reclaimed;

	    public T Get(EnemyType enemyType, Vector2 position, Vector2 direction)
	    {
		    T enemy = GetInstance(enemyType);

		    enemy.FactoryGameElements = this;
		    
		    enemy.transform.position = position;
		    enemy.Direction = direction;
		    
		    return enemy;
	    }

	    protected override void Reclaim(T element)
	    {
		    Reclaimed?.Invoke(element);
	    }

	    protected abstract T GetInstance(EnemyType enemyType);
    }
}
