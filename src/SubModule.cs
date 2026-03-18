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
                string configError = Config.GatherConfigurationData(); 
                if (!configError.IsEmpty())
                {
                    InformationManager.DisplayMessage(new InformationMessage(
                        "!!Configuration Error - ScoutingEffectMovSpeed using default values.", Colors.Red));
                    InformationManager.DisplayMessage(new InformationMessage(configError, Colors.Red));
                }
                else
                {
                    InformationManager.DisplayMessage(new InformationMessage(
                        "ScoutingEffectMovSpeed v2.1.0 loaded", Colors.Green));
                };

                starterObject.AddModel(new ScoutingSpeedCalculatingModel());

                if (Config.gainBonusXP)
                {
                    campaignStarter.AddBehavior(new ScoutingskillMovmentBehavior());
                }
            }
        }
    }
}
