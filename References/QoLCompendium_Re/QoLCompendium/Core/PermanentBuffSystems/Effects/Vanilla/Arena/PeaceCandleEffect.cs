using System;
using Terraria;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena
{
	// Token: 0x0200039B RID: 923
	public class PeaceCandleEffect : IPermanentBuff
	{
		// Token: 0x06001363 RID: 4963 RVA: 0x0008E9B0 File Offset: 0x0008CBB0
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[157] && !PermanentBuffPlayer.PermanentBuffsBools[5])
			{
				player.Player.ZonePeaceCandle = true;
				if (Main.myPlayer == player.Player.whoAmI)
				{
					Main.SceneMetrics.PeaceCandleCount = 0;
				}
				player.Player.buffImmune[157] = true;
			}
		}
	}
}
