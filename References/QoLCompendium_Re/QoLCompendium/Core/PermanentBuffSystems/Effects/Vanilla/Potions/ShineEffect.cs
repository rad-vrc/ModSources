using System;
using Terraria;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000382 RID: 898
	public class ShineEffect : IPermanentBuff
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x0008E1D0 File Offset: 0x0008C3D0
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[11] && !PermanentBuffPlayer.PermanentBuffsBools[40])
			{
				Lighting.AddLight((int)(player.Player.position.X + (float)(player.Player.width / 2)) / 16, (int)(player.Player.position.Y + (float)(player.Player.height / 2)) / 16, 0.8f, 0.95f, 1f);
				player.Player.buffImmune[11] = true;
			}
		}
	}
}
