using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ScoutingEffectMovSpeed
{
    public class SubModule : MBSubModuleBase
    {
        internal static ConfigurationManager Config { get; private set; }

        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            if (starterObject is CampaignGameStarter campaignStarter)
            {
                Config = new ConfigurationManager();
                Config.GatherConfigurationData();

                starterObject.AddModel(new ScoutingSpeedCalculatingModel());
                InformationManager.DisplayMessage(new InformationMessage("ScoutingEffectMovSpeed v2.0.2 loaded", Colors.Green));

                if (Config.SEMScostumSpEXP)
                {
                    campaignStarter.AddBehavior(new ScoutingskillMovmentBehavior());
                }
            }
        }
    }
}
