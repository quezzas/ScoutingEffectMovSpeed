using System;
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

        public void GiveScoutingXp(MobileParty party)
        {
            if (party.LeaderHero == null || !party.IsMoving || party.EffectiveScout == null)
                return;

            TerrainType terrain = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(party.CurrentNavigationFace);
            float baseXp = 5f;
            float xp;

            switch ((int)terrain)
            {
                case 0:  // Water
                    xp = baseXp * 4f;
                    break;
                case 1:  // Mountain
                    xp = baseXp * 3f;
                    break;
                case 2:  // Snow
                    xp = baseXp * 3f;
                    break;
                case 3:  // Steppe
                    xp = baseXp * 2f;
                    break;
                case 4:  // Plain
                    xp = baseXp * 1f;
                    break;
                case 5:  // Desert
                    xp = baseXp * 2f;
                    break;
                case 6:  // Swamp
                    xp = baseXp * 2f;
                    break;
                case 7:  // Dune
                    xp = baseXp * 3f;
                    break;
                case 8:  // Lake
                    xp = baseXp * 1f;
                    break;
                case 9:  // River
                    xp = baseXp * 1f;
                    break;
                case 10: // Forest
                    xp = baseXp * 2f;
                    break;
                case 11: // ShallowRiver
                    xp = baseXp * 1f;
                    break;
                case 12: // Canyon
                    xp = baseXp * 1f;
                    break;
                case 13: // RuralArea
                    xp = baseXp * 1f;
                    break;
                case 14: // Flat
                    xp = baseXp * 1f;
                    break;
                default:
                    xp = baseXp;
                    break;
            }

            party.EffectiveScout.AddSkillXp(DefaultSkills.Scouting, xp);
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}
