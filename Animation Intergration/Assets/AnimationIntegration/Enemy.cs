using System;
using System.Collections;
using UnityEngine;

namespace AnimationIntegration
{
    public class Enemy : MonoBehaviour
    {
        public event Action Died;
        
        public GameObject Root;
        public Animator Animator;
        public Transform KillingPoint;
        public int TimeDisappearance;

        private Rigidbody[] _rigidbodies;

        private void Awake()
        {
            _rigidbodies = Root.GetComponentsInChildren<Rigidbody>();

            MakePhysical(false);
        }

        public void Kill()
        {
            MakePhysical(true);

            StartCoroutine(DestroyWithDelay());
        }

        private void MakePhysical(bool isPhysic)
        {
            Animator.enabled = !isPhysic;

            foreach (var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = !isPhysic;
            }
        }

        private IEnumerator DestroyWithDelay()
        {
            yield return new WaitForSeconds(TimeDisappearance);

            Died?.Invoke();
            
            Destroy(gameObject);
        }
    }
}