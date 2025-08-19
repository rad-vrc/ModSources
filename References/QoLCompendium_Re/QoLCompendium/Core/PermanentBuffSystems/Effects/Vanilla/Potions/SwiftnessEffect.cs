using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000386 RID: 902
	public class SwiftnessEffect : IPermanentBuff
	{
		// Token: 0x06001339 RID: 4921 RVA: 0x0008E30C File Offset: 0x0008C50C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[3] && !PermanentBuffPlayer.PermanentBuffsBools[44])
			{
				player.Player.moveSpeed += 0.25f;
				player.Player.buffImmune[3] = true;
			}
		}
	}
}
