using System;
using System.Reflection;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000237 RID: 567
	public class PillarsDropMore : GlobalNPC
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x0006C8C4 File Offset: 0x0006AAC4
		private static void MakeLunarPillarDropRulesBetter(DropOneByOne drop)
		{
			PropertyInfo property = typeof(DropOneByOne).GetProperty("parameters", BindingFlags.Instance | BindingFlags.Public);
			DropOneByOne.Parameters newParameters = new DropOneByOne.Parameters
			{
				MinimumItemDropsCount = 90,
				MaximumItemDropsCount = 110,
				ChanceNumerator = 1,
				ChanceDenominator = 1,
				MinimumStackPerChunkBase = 1,
				MaximumStackPerChunkBase = 1,
				BonusMinDropsPerChunkPerPlayer = 1,
				BonusMaxDropsPerChunkPerPlayer = 1
			};
			property.SetValue(drop, newParameters);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0006C940 File Offset: 0x0006AB40
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (Common.LunarPillarIDs.Contains(npc.type) && QoLCompendium.mainConfig.LunarPillarsDropMoreFragments)
			{
				foreach (IItemDropRule itemDropRule in npcLoot.Get(true))
				{
					DropBasedOnExpertMode expertRule = itemDropRule as DropBasedOnExpertMode;
					if (expertRule != null)
					{
						DropOneByOne normalDrop = expertRule.ruleForNormalMode as DropOneByOne;
						if (normalDrop != null)
						{
							PillarsDropMore.MakeLunarPillarDropRulesBetter(normalDrop);
						}
						DropOneByOne expertDrop = expertRule.ruleForExpertMode as DropOneByOne;
						if (expertDrop != null)
						{
							PillarsDropMore.MakeLunarPillarDropRulesBetter(expertDrop);
						}
					}
				}
			}
		}
	}
}
