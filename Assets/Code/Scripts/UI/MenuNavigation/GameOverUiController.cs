using Code.Scripts.GameServices;
using UnityEngine.SceneManagement;

namespace Code.Scripts.UI.MenuNavigation
{
    public class GameOverUiController
    {
        private readonly GameOverUi ui;
        private readonly GameFlowService gameFlowService;

        public GameOverUiController(GameOverUi ui, GameFlowService gameFlowService)
        {
            this.ui = ui;
            this.gameFlowService = gameFlowService;
            
            gameFlowService.OnGameOver += OnGameOver;

            ui.ToMainMenuButton.onClick.AddListener(OpenMainMenu);
            ui.RestartButton.onClick.AddListener(Restart);
        }

        private void OnGameOver(GameOverReason reason)
        {
            ui.SetReason(reason);
            ui.SetVisible(true);
        }

        private void OpenMainMenu()
        {
            SceneManager.LoadScene("Scenes/UI/StartScreen");
        }
        
        private void Restart()
        {
            SceneManager.LoadScene("Scenes/Level_0");
        }
        
    }
}