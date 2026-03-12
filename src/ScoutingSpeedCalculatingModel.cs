using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
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
                float bonus = config.SEMSFactor * scoutSkill;
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
            string[] groups = config.SEMSappliedto.Split(';');

            foreach (string group in groups)
            {
                switch (group.Trim())
                {
                    case "1":
                        if (mobileParty.LeaderHero == Hero.MainHero) return true;
                        break;
                    case "2":
                        if (mobileParty.MapFaction == Hero.MainHero.MapFaction) return true;
                        break;
                    case "3":
                        if (mobileParty.EffectiveScout != null) return true;
                        break;
                    case "4":
                        if (mobileParty.IsLordParty) return true;
                        break;
                    case "5":
                        if (mobileParty.ActualClan == Clan.PlayerClan) return true;
                        break;
                }
            }

            return false;
        }
    }
}
