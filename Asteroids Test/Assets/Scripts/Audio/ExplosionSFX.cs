using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class ExplosionSFX : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Init(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);

            StartCoroutine(WaitForExplosionClipToEnd(clip.length));
        }

        private IEnumerator WaitForExplosionClipToEnd(float time)
        {
            yield return new WaitForSeconds(time);
            
            Destroy(gameObject);
        }
        
    }
}