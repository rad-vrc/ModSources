using System;
using Terraria;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena
{
	// Token: 0x02000397 RID: 919
	public class CampfireEffect : IPermanentBuff
	{
		// Token: 0x0600135B RID: 4955 RVA: 0x0008E878 File Offset: 0x0008CA78
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[87] && !PermanentBuffPlayer.PermanentBuffsBools[1])
			{
				if (Main.myPlayer == player.Player.whoAmI || Main.netMode == 2)
				{
					Main.SceneMetrics.HasCampfire = true;
				}
				player.Player.buffImmune[87] = true;
			}
		}
	}
}
