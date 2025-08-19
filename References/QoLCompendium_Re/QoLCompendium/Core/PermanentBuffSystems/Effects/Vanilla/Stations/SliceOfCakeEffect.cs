using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations
{
	// Token: 0x02000362 RID: 866
	public class SliceOfCakeEffect : IPermanentBuff
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x0008D4F4 File Offset: 0x0008B6F4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[192] && !PermanentBuffPlayer.PermanentBuffsBools[56])
			{
				player.Player.pickSpeed -= 0.2f;
				player.Player.moveSpeed += 0.2f;
				player.Player.buffImmune[192] = true;
			}
		}
	}
}
