using System;
using SibGameJam.ScriptableObjects;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using SibGameJam.TankFactorySpace;
using UnityEngine;

namespace SibGameJam.GameServices
{
    public class FactoryUpgradeManager
    {
        public int CurrentLevel { get; private set; }
        public int NextLevelLevelPoints { get; private set; }
        public int PointsInCurrentLevel { get; private set; }
        public event Action<int, PlayerBonus[]> OnLevelUp;
        public event Action<int, int> OnPointsAdded;

        private int _currentPoints;
        private readonly FactoryUpgradeSettings _settings;

        public FactoryUpgradeManager(FactoryUpgradeSettings settings)
        {
            _settings = settings;
            UpdateCurrentLevelState();
        }

        public void AddUpgradePoints(ResourceType resourceType, int count)
        {
            _currentPoints += count;
            UpdateCurrentLevelState();
        }

        private void UpdateCurrentLevelState()
        {
            for (var i = 0; i < _settings.Levels.Length; i++)
            {
                var nextLevel = _settings.Levels[i];
                var currentLevel = i - 1 >= 0 ? _settings.Levels[i - 1] : null;
                if (nextLevel.PointCost > _currentPoints)
                {
                    if (i > CurrentLevel)
                    {
                        OnLevelUp?.Invoke(i, currentLevel.UpgradeVariants);
                    }
                    
                    CurrentLevel = i;
                    PointsInCurrentLevel = _currentPoints - (currentLevel?.PointCost ?? 0);
                    NextLevelLevelPoints = nextLevel.PointCost - (currentLevel?.PointCost ?? 0);
                    OnPointsAdded?.Invoke(PointsInCurrentLevel, NextLevelLevelPoints);
                    Debug.Log($"Update levels. Current level: {CurrentLevel} points in current level: {PointsInCurrentLevel} next level points {NextLevelLevelPoints} total points: {_currentPoints}");
                    return;
                }
            }
        }
    }
}