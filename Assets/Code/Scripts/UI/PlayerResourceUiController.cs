using System.Linq;
using FS.Gameplay.PlayerVehicle;
using SibGameJam.TankFactorySpace;
using VContainer.Unity;

namespace Code.Scripts.Ui
{
    public class PlayerResourceUiController: IPostStartable
    {
        private readonly PlayerResourcesUi ui;
        private readonly VehicleController player;
        
        private ResourceHolder PlayerResources => player.ResourceBag.Resources;

        public PlayerResourceUiController(PlayerResourcesUi ui, VehicleController player)
        {
            this.ui = ui;
            this.player = player;
        }
        
        public void PostStart()
        {
            PlayerResources.OnResourceCountChanged += UpdateUi;
            UpdateUi(ResourceType.AllyWreck, 0, 0);
        }

        private void UpdateUi(ResourceType type, int currentCount, int delta)
        {
            ui.CurrentResourcesText.text = $"{PlayerResources.Resources.Values.Sum()} / {PlayerResources.Capacity}";
        }
    }
}