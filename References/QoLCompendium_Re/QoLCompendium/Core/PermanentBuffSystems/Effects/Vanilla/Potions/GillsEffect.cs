using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000371 RID: 881
	public class GillsEffect : IPermanentBuff
	{
		// Token: 0x0600130F RID: 4879 RVA: 0x0008DA1A File Offset: 0x0008BC1A
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[4] && !PermanentBuffPlayer.PermanentBuffsBools[23])
			{
				player.Player.gills = true;
				player.Player.buffImmune[4] = true;
			}
		}
	}
}
