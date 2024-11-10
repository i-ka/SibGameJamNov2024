using Code.Scripts.GameServices;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Scripts.UI.MenuNavigation
{
    public class GameOverUiController: IInitializable
    {
        private readonly InGameMenu ui;
        private readonly GameFlowService gameFlowService;

        public GameOverUiController(InGameMenu ui, GameFlowService gameFlowService)
        {
            this.ui = ui;
            this.gameFlowService = gameFlowService;
        }
        
        public void Initialize()
        {
            gameFlowService.OnGameOver += OnGameOver;
        }

        private void OnGameOver(GameOverReason reason)
        {
            if (reason is GameOverReason.Win)
                ui.OpenWinDialog();
            
            ui.OpenLoseDialog();
        }
    }
}