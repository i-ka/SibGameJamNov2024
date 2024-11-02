using System;
using SibGameJam.ScriptableObjects;
using UnityEngine;

namespace SibGameJam
{
    public class ResearchManager
    {
        public int CurrentLevel { get; private set; }
        public int NextLevelLevelPoints { get; private set; }
        public int PointsInCurrentLevel { get; private set; }

        private readonly PlayerLevelingConfiguration _playerLevelingConfiguration;
        private int _currentPoints;
        private event Action<int> OnLevelUp;

        public ResearchManager(PlayerLevelingConfiguration levelingConfiguration)
        {
            _playerLevelingConfiguration = levelingConfiguration;
        }

        public void AddResearchPoints(int researchPoints)
        {
            _currentPoints += researchPoints;
            UpdateCurrentLevelState();
        }


        private void UpdateCurrentLevelState()
        {
            for (var i = 0; i < _playerLevelingConfiguration.Levels.Count; i++)
            {
                var nextLevelPoints = _playerLevelingConfiguration.Levels[i];
                if (nextLevelPoints >= _currentPoints)
                {
                    if (i > CurrentLevel)
                        OnLevelUp?.Invoke(i);
                    
                    CurrentLevel = i;
                    PointsInCurrentLevel = _currentPoints - (i - 1 > 0 ? _playerLevelingConfiguration.Levels[i - 1] : 0);
                    NextLevelLevelPoints = nextLevelPoints - (i - 1 > 0 ? _playerLevelingConfiguration.Levels[i - 1] : 0);
                    Debug.Log($"Update levels. Current level: {CurrentLevel} points in current level: {PointsInCurrentLevel} next level points {NextLevelLevelPoints} total points: {_currentPoints}");
                    return;
                }
            }
        }
    }
}