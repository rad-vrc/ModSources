using System;
using Terraria;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena
{
	// Token: 0x0200039F RID: 927
	public class WaterCandleEffect : IPermanentBuff
	{
		// Token: 0x0600136B RID: 4971 RVA: 0x0008EB64 File Offset: 0x0008CD64
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[86] && !PermanentBuffPlayer.PermanentBuffsBools[9])
			{
				player.Player.ZoneWaterCandle = true;
				if (Main.myPlayer == player.Player.whoAmI)
				{
					Main.SceneMetrics.WaterCandleCount = 0;
				}
				player.Player.buffImmune[86] = true;
			}
		}
	}
}
