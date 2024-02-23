using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the communication between the health slider and the health events.
/// This is also where we may add additional functionality if it was relevant to the Target.
/// We start listening to the Health events in OnEnable, and stop listening in OnDisable.
/// </summary>
namespace Examples.Observer
{
    [RequireComponent(typeof(Health))]
    public class Target : MonoBehaviour
    {
        [SerializeField] Slider _healthSlider = null;

        public Health Health { get; private set; }

        private void Awake()
        {
            Health = GetComponent<Health>();

            _healthSlider.maxValue = Health.MaxHealth;
            _healthSlider.value = Health.StartingHealth;
        }

        private void OnEnable()
        {
            // subscribe to get notified when this health takes damage!
        }

        private void OnDisable()
        {
            //Unsubscieb to the event      
        }

        void DisplayDamage(int damage)
        {
            // on damaged, display the new health
            _healthSlider.value = Health.CurrentHealth;
        }
    }
}

