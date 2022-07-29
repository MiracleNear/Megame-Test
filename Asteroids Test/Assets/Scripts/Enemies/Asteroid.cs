using System;
using Factories;
using UnityEngine;

namespace Enemies
{
    public class Asteroid : Enemy
    {
        public int t;

        private void Update()
        {
            Move();
        }

        private void OnCollisionEnter2D(Collision2D other)
        { 
            if (other.gameObject.TryGetComponent(out IAsteroidCollisionHandler handler))
            {
                handler.OnCollisionAsteroid();
                PlaySoundDeath();
            }
        }
    }
}