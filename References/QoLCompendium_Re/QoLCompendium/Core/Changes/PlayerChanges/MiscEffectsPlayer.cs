using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.PlayerChanges
{
	// Token: 0x0200022F RID: 559
	public class MiscEffectsPlayer : ModPlayer
	{
		// Token: 0x06000D71 RID: 3441 RVA: 0x000687E8 File Offset: 0x000669E8
		public override void OnEnterWorld()
		{
			if (base.Player != Main.player[Main.myPlayer])
			{
				return;
			}
			if (QoLCompendium.mainConfig.AutoTeams > 0 && !this.joinedTeam && Main.netMode == 1)
			{
				Main.player[Main.myPlayer].team = QoLCompendium.mainConfig.AutoTeams;
				this.joinedTeam = true;
				NetMessage.SendData(45, -1, -1, null, Main.myPlayer, (float)QoLCompendium.mainConfig.AutoTeams, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00068870 File Offset: 0x00066A70
		public override void PostUpdateMiscEffects()
		{
			base.Player.tileSpeed -= QoLCompendium.mainConfig.IncreasePlaceSpeed;
			base.Player.wallSpeed -= QoLCompendium.mainConfig.IncreasePlaceSpeed;
			Player.tileRangeX += QoLCompendium.mainConfig.IncreasePlaceRange;
			Player.tileRangeY += QoLCompendium.mainConfig.IncreasePlaceRange;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x000688E0 File Offset: 0x00066AE0
		public override void PostUpdateEquips()
		{
			if (ModContent.GetInstance<QoLCConfig>().NoExpertIceWaterChilled && base.Player.wet && base.Player.ZoneSnow && Main.expertMode)
			{
				base.Player.buffImmune[46] = true;
				base.Player.chilled = false;
			}
			if (ModContent.GetInstance<QoLCConfig>().NoShimmerSink && base.Player.wet)
			{
				base.Player.buffImmune[353] = true;
				base.Player.shimmerImmune = true;
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0006896C File Offset: 0x00066B6C
		public override void PreUpdateBuffs()
		{
			if (QoLCompendium.mainConfig.DisableHappiness)
			{
				base.Player.currentShoppingSettings.PriceAdjustment = (double)QoLCompendium.mainConfig.HappinessPriceChange;
			}
		}

		// Token: 0x0400059C RID: 1436
		public bool joinedTeam;
	}
}
