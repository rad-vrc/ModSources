using System;
using CalamityMod.World;
using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core
{
	// Token: 0x02000202 RID: 514
	public class ExpertOnlyDropCondition : IItemDropRuleCondition, IProvideItemConditionDescription
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0004E8DB File Offset: 0x0004CADB
		[JITWhenModsEnabled(new string[]
		{
			"CalamityMod"
		})]
		public static bool RevengeanceMode
		{
			get
			{
				return CalamityWorld.revenge;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0004E8E2 File Offset: 0x0004CAE2
		[JITWhenModsEnabled(new string[]
		{
			"CalamityMod"
		})]
		public static bool DeathMode
		{
			get
			{
				return CalamityWorld.death;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0004E8E9 File Offset: 0x0004CAE9
		[JITWhenModsEnabled(new string[]
		{
			"FargowiltasSouls"
		})]
		public static bool EternityMode
		{
			get
			{
				return WorldSavingSystem.EternityMode;
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0004E8F0 File Offset: 0x0004CAF0
		public bool CanDrop(DropAttemptInfo info)
		{
			return info.npc.boss && QoLCompendium.mainConfig.RelicsInExpert && !Main.masterMode && !ExpertOnlyDropCondition.CalamityDifficultyEnabled() && !ExpertOnlyDropCondition.FargoSoulsDifficultyEnabled() && Main.expertMode;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0004E927 File Offset: 0x0004CB27
		public bool CanShowItemDropInUI()
		{
			return Main.expertMode && QoLCompendium.mainConfig.RelicsInExpert && !Main.masterMode && !ExpertOnlyDropCondition.CalamityDifficultyEnabled() && !ExpertOnlyDropCondition.FargoSoulsDifficultyEnabled();
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0004E954 File Offset: 0x0004CB54
		public string GetConditionDescription()
		{
			return Language.GetTextValue("Mods.QoLCompendium.NPCDropConditions.ExpertNotMaster");
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0004E960 File Offset: 0x0004CB60
		public static bool CalamityDifficultyEnabled()
		{
			return ModLoader.HasMod("CalamityMod") && (ExpertOnlyDropCondition.RevengeanceMode || ExpertOnlyDropCondition.DeathMode);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0004E97F File Offset: 0x0004CB7F
		public static bool FargoSoulsDifficultyEnabled()
		{
			return ModLoader.HasMod("FargowiltasSouls") && ExpertOnlyDropCondition.EternityMode;
		}
	}
}
