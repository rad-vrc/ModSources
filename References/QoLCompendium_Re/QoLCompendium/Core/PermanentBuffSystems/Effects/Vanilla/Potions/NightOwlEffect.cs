using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200037D RID: 893
	public class NightOwlEffect : IPermanentBuff
	{
		// Token: 0x06001327 RID: 4903 RVA: 0x0008DF91 File Offset: 0x0008C191
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[12] && !PermanentBuffPlayer.PermanentBuffsBools[35])
			{
				player.Player.nightVision = true;
				player.Player.buffImmune[12] = true;
			}
		}
	}
}
