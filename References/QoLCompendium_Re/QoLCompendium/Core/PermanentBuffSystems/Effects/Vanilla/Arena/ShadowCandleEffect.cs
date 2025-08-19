using System;
using Terraria;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena
{
	// Token: 0x0200039C RID: 924
	public class ShadowCandleEffect : IPermanentBuff
	{
		// Token: 0x06001365 RID: 4965 RVA: 0x0008EA14 File Offset: 0x0008CC14
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[350] && !PermanentBuffPlayer.PermanentBuffsBools[6])
			{
				player.Player.ZoneShadowCandle = true;
				if (Main.myPlayer == player.Player.whoAmI)
				{
					Main.SceneMetrics.ShadowCandleCount = 0;
				}
				player.Player.buffImmune[350] = true;
			}
		}
	}
}
