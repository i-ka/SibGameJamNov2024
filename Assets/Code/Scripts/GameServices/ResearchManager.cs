using System;
using SibGameJam.ScriptableObjects;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SibGameJam
{
    public class ResearchManager
    {
        public int CurrentLevel { get; private set; }
        public int NextLevelLevelPoints { get; private set; }
        public int PointsInCurrentLevel { get; private set; }

        private readonly PlayerLevelingConfiguration _playerLevelingConfiguration;
        private readonly IObjectResolver _objectResolver;
        private int _currentPoints;

        public event Action<int, PlayerBonus> OnLevelUp;
        public event Action<int, int> OnPointsAdded;

        public ResearchManager(PlayerLevelingConfiguration levelingConfiguration, IObjectResolver objectResolver)
        {
            _playerLevelingConfiguration = levelingConfiguration;
            _objectResolver = objectResolver;
            UpdateCurrentLevelState();
        }

        public void AddResearchPoints(int researchPoints)
        {
            _currentPoints += researchPoints;
            UpdateCurrentLevelState();
            OnPointsAdded?.Invoke(PointsInCurrentLevel, NextLevelLevelPoints);
        }

        private void UpdateCurrentLevelState()
        {
            for (var i = 0; i < _playerLevelingConfiguration.Levels.Count; i++)
            {
                var nextLevel = _playerLevelingConfiguration.Levels[i];
                var currentLevel = i - 1 >= 0 ? _playerLevelingConfiguration.Levels[i - 1] : null;
                if (nextLevel.points >= _currentPoints)
                {
                    if (i > CurrentLevel)
                    {
                        currentLevel.bonus.Apply(_objectResolver);
                        OnLevelUp?.Invoke(i, currentLevel.bonus);
                    }

                    CurrentLevel = i;
                    PointsInCurrentLevel = _currentPoints - (i - 1 >= 0 ? _playerLevelingConfiguration.Levels[i - 1].points : 0);
                    NextLevelLevelPoints = nextLevel.points - (i - 1 >= 0 ? _playerLevelingConfiguration.Levels[i - 1].points : 0);
                    Debug.Log($"Update levels. Current level: {CurrentLevel} points in current level: {PointsInCurrentLevel} next level points {NextLevelLevelPoints} total points: {_currentPoints}");
                    return;
                }
            }
        }
    }
}