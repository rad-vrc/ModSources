using System;
using Terraria;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena
{
	// Token: 0x02000399 RID: 921
	public class HeartLanternEffect : IPermanentBuff
	{
		// Token: 0x0600135F RID: 4959 RVA: 0x0008E8F4 File Offset: 0x0008CAF4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[89] && !PermanentBuffPlayer.PermanentBuffsBools[3])
			{
				if (Main.myPlayer == player.Player.whoAmI || Main.netMode == 2)
				{
					Main.SceneMetrics.HasHeartLantern = true;
				}
				player.Player.buffImmune[89] = true;
			}
		}
	}
}
