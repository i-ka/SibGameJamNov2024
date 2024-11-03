using System;
using System.Collections;
using System.Linq;
using Code.Scripts.AI.Brain;
using Code.Scripts.AI.Data;
using SibGameJam;
using Unity.VisualScripting;
using UnityEngine;

namespace FS.Gameplay.PlayerVehicle
{
    public class RepairAbility : MonoBehaviour, IPlayerAbility
    {
        public event Action<float> OnProgressChanged;
        public event Action OnFinished;
        
        [SerializeField] private Transform _abilityPosition;
        [SerializeField] private float _radius;
        [SerializeField] private float _duration = 3;
        [SerializeField] private float _cooldown;
        [SerializeField] private LayerMask _applyToLayerMask;

        [SerializeField] private int _maxCharges = 2;
        
        
        private int _currentCharges;
        private float _useProgress;

        private void Start()
        {
            _currentCharges = _maxCharges;
        }

        public bool CanUse()
        {
            if (_currentCharges == 0) return false;
            
            var abilityTargets = Physics.OverlapSphere(_abilityPosition.position, _radius, _applyToLayerMask);
            foreach (var target in abilityTargets)
            {
                var tank = target.GetComponent<ITank>();
                if (tank == null) continue;

                if (tank.Team == Team.Blue)
                {
                    return true;
                }
            }

            return false;
        }

        public void Use()
        {
            var abilityTargets = Physics.OverlapSphere(_abilityPosition.position, _radius, _applyToLayerMask);
            var target = abilityTargets.Select(t => t.GetComponent<ITank>()).FirstOrDefault(t => t?.Team == Team.Blue);
            StartCoroutine(Repair(target));
        }

        private IEnumerator Repair(ITank target)
        {
            while (_useProgress < _duration)
            {
                _useProgress += Time.deltaTime;
                OnProgressChanged?.Invoke(_useProgress);
                yield return new WaitForEndOfFrame();
            }

            _currentCharges--;
            target.HealthController.RestoreHealth();
            OnFinished?.Invoke();
            Debug.Log("Repair ability applied");
            StartCoroutine(RestoreCharge());
        }

        private IEnumerator RestoreCharge()
        {
            yield return new WaitForSeconds(_cooldown);
            Debug.Log("Repair ability charge restored");
            _currentCharges++;
        }
    }
}