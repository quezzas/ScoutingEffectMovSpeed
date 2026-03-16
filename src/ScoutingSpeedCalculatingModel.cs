using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace ScoutingEffectMovSpeed
{
    public class ScoutingSpeedCalculatingModel : DefaultPartySpeedCalculatingModel
    {
        private static readonly TextObject _scoutingBonus = new TextObject("{=SEMS172str0}Scouting Bonus");

        public override ExplainedNumber CalculateFinalSpeed(MobileParty mobileParty, ExplainedNumber finalSpeed)
        {
            // Let the base game handle all vanilla speed calculations
            // (terrain, weather, perks, cavalry, cargo, morale, etc.)
            ExplainedNumber result = base.CalculateFinalSpeed(mobileParty, finalSpeed);

            // Apply custom scouting speed bonus
            ConfigurationManager config = SubModule.Config;

            if (config != null && Hero.MainHero != null && ShouldApplyBonus(mobileParty, config) && mobileParty.EffectiveScout != null)
            {
                float scoutSkill = mobileParty.EffectiveScout.GetSkillValue(DefaultSkills.Scouting);
                float bonus = config.speedFactor * scoutSkill;
                if (bonus > 0f)
                {
                    result.AddFactor(bonus, _scoutingBonus);
                }
            }

            result.LimitMin(MinimumSpeed);
            return result;
        }

        private bool ShouldApplyBonus(MobileParty mobileParty, ConfigurationManager config)
        {
            // Ordered from most to least likely condition
            return       (config.appliesTo[2] == 1 && mobileParty.EffectiveScout != null)
                      || (config.appliesTo[3] == 1 && mobileParty.IsLordParty)
                      || (config.appliesTo[1] == 1 && mobileParty.MapFaction == Hero.MainHero.MapFaction)
                      || (config.appliesTo[4] == 1 && mobileParty.ActualClan == Clan.PlayerClan) 
                      || (config.appliesTo[0] == 1 && mobileParty.LeaderHero == Hero.MainHero );
        }
    }
}
