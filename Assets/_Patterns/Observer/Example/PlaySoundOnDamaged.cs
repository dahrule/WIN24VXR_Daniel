﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script uses the observer pattern to play sound effects
/// whenever the observed health takes damage or is killed.
/// Note that this script doesn't know about any other scripts
/// or Observers other than the Health, our Subject.
/// Usually you may want to combine this into other class behavior, but I think this is a useful
/// way of seeing how event calls allow you to break down classes as much as you need.
/// </summary>
namespace Examples.Observer
{
    [RequireComponent(typeof(Health))]
    public class PlaySoundOnDamaged : MonoBehaviour
    {
        [SerializeField] AudioClip _damagedSound = null;
        [SerializeField] AudioClip _killedSound = null;
        [SerializeField] Transform _locationToPlay = null;

        Health _health = null;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnDamaged += PlayDamageSound;
            _health.OnKilled += PlayKillSound;
        }

        private void OnDisable()
        {
            ///TODO Unregister to events
        }

        void PlayDamageSound(int damage)
        {
            if(_damagedSound != null && _locationToPlay != null)
            {
                AudioSource.PlayClipAtPoint(_damagedSound, _locationToPlay.position);
            }
        }

        void PlayKillSound()
        {
            if (_damagedSound != null && _locationToPlay != null)
            {
                AudioSource.PlayClipAtPoint(_killedSound, _locationToPlay.position);
            }
        }
    }
}

