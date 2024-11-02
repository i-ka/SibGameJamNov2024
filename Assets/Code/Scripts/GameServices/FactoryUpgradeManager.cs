using System;
using SibGameJam.ScriptableObjects;
using SibGameJam.TankFactory;
using UnityEngine;

namespace SibGameJam.GameServices
{
    public class FactoryUpgradeManager
    {
        public int CurrentLevel { get; private set; }
        public int NextLevelLevelPoints { get; private set; }
        public int PointsInCurrentLevel { get; private set; }
        public event Action<int, FactoryUpgrade[]> OnLevelUp;
        public event Action<int, int> OnPointsAdded;

        private int _currentPoints;
        private readonly FactoryUpgradeSettings _settings;

        public FactoryUpgradeManager(FactoryUpgradeSettings settings)
        {
            _settings = settings;
        }

        public void AddUpgradePoints(ResourceType resourceType, int count)
        {
            if (!_settings.ResourcesCost.TryGetValue(resourceType, out var cost)) 
            {
                Debug.Log($"Unsupported resource type {resourceType} for factory upgrade");
                return;
            }

            _currentPoints += count;
            UpdateCurrentLevelState();
        }

        private void UpdateCurrentLevelState()
        {
            for (var i = 0; i < _settings.Levels.Length; i++)
            {
                var nextLevel = _settings.Levels[i];
                if (nextLevel.PointCost >= _currentPoints)
                {
                    if (i > CurrentLevel)
                        OnLevelUp?.Invoke(i, _settings.Levels[i].UpgradeVariants);
                    
                    CurrentLevel = i;
                    PointsInCurrentLevel = _currentPoints - (i - 1 > 0 ? _settings.Levels[i - 1].PointCost : 0);
                    NextLevelLevelPoints = nextLevel.PointCost - (i - 1 > 0 ? _settings.Levels[i - 1].PointCost : 0);
                    Debug.Log($"Update levels. Current level: {CurrentLevel} points in current level: {PointsInCurrentLevel} next level points {NextLevelLevelPoints} total points: {_currentPoints}");
                    return;
                }
            }
        }

    }
}