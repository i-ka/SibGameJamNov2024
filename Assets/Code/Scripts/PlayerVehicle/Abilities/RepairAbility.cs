using System;
using System.Collections;
using System.Linq;
using Code.Scripts.AI.Brain;
using Code.Scripts.AI.Data;
using SibGameJam;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Gameplay.PlayerVehicle
{
    public class RepairAbility : PlayerAbility
    {
        [SerializeField] private Transform _abilityPosition;
        [SerializeField] private float _radius;
        [SerializeField] private float _duration = 3;
        [SerializeField] private float _cooldown;
        [SerializeField] private LayerMask _applyToLayerMask;

        [SerializeField] private int _maxCharges = 2;

        [SerializeField] private bool _applyInProgress = false;

        [SerializeField] private VehicleSoundController _soundController;
        
        
        private int _currentCharges;
        private float _useProgress;

        private void Start()
        {
            _currentCharges = _maxCharges;
        }

        public override bool CanUse()
        {
            if (_currentCharges == 0) return false;
            if (_applyInProgress) return false;
            
            var abilityTargets = Physics.OverlapSphere(_abilityPosition.position, _radius, _applyToLayerMask);
            foreach (var target in abilityTargets)
            {
                var tank = target.GetComponent<ITank>();
                if (tank == null) continue;

                if (tank.Team == Team.Red)
                {
                    return true;
                }
            }

            return false;
        }

        public override void Use()
        {
            var abilityTargets = Physics.OverlapSphere(_abilityPosition.position, _radius, _applyToLayerMask);
            var target = abilityTargets
                .Select(t => t.GetComponent<ITank>())
                .OrderBy(t => t.HealthController.CurrentHealth)
                .FirstOrDefault(t => t?.Team == Team.Red);
            StartCoroutine(Repair(target));
        }

        public void AddCharges(int chargeCount)
        {
            _maxCharges += chargeCount;
            for (int i = 0; i < chargeCount; i++)
                StartCoroutine(RestoreCharge());
        }

        private IEnumerator Repair(ITank target)
        {
            Debug.Log("Start repair");
            _applyInProgress = true;
            while (_useProgress < _duration)
            {
                _useProgress += Time.deltaTime;
                EmitProgressChanged(_useProgress);
                yield return new WaitForEndOfFrame();
            }

            _currentCharges--;
            target.HealthController.RestoreHealth();
            EmitFinished();
            _soundController.PlayTankRepairSound();
            Debug.Log("Repair ability applied");
            _applyInProgress = false;
            _useProgress = 0;
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