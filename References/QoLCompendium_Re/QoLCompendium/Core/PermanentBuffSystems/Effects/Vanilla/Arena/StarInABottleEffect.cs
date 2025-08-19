using System;
using Terraria;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena
{
	// Token: 0x0200039D RID: 925
	public class StarInABottleEffect : IPermanentBuff
	{
		// Token: 0x06001367 RID: 4967 RVA: 0x0008EA78 File Offset: 0x0008CC78
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[158] && !PermanentBuffPlayer.PermanentBuffsBools[7])
			{
				if (Main.myPlayer == player.Player.whoAmI || Main.netMode == 2)
				{
					Main.SceneMetrics.HasStarInBottle = true;
				}
				player.Player.manaRegenBonus += 2;
				player.Player.buffImmune[158] = true;
			}
		}
	}
}
