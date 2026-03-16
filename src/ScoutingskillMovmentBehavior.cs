using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace ScoutingEffectMovSpeed
{
    public class ScoutingskillMovmentBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.HourlyTickPartyEvent.AddNonSerializedListener(this, GiveScoutingXp);
        }
        
        private const float BaseXp = 5f;

        public void GiveScoutingXp(MobileParty party)
        {
            if (party.LeaderHero == null || !party.IsMoving || party.EffectiveScout == null)
                return;

            TerrainType terrain = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(party.CurrentNavigationFace);
            float bonusXpMult;

            switch (terrain)
            {
                case TerrainType.Water:
                    bonusXpMult = 3f;
                    break;
                case TerrainType.Mountain:
                case TerrainType.Dune:
                case TerrainType.Snow:
                    bonusXpMult = 2f;
                    break;
                case TerrainType.Steppe:
                case TerrainType.Desert:
                case TerrainType.Swamp:
                case TerrainType.River:
                case TerrainType.Forest:
                    bonusXpMult = 1f;
                    break;
                default:
                    return;
            }

            float extraXp = SubModule.Config.bonusXpFactor * bonusXpMult * BaseXp;
            party.EffectiveScout.AddSkillXp(DefaultSkills.Scouting, extraXp);
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}
