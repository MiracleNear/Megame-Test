using System;
using Enemies;
using Factories;
using Spawner;
using UnityEngine;

[RequireComponent(typeof(UfoSpawner))]
public class UfoGenerator : EnemyGenerator<Ufo>
{
    [SerializeField] private float _minAppearanceTime, _maxAppearanceTime;
    
    public override void Init(Action<Ufo> generated, Action<Ufo> removed)
    {
        EnemySpawner = GetComponent<UfoSpawner>();
        
        base.Init(generated, removed);
    }

    public override void Generate(EnemyType enemyType, int count)
    {
        
    }
}
