using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000367 RID: 871
	public class BiomeSightEffect : IPermanentBuff
	{
		// Token: 0x060012FB RID: 4859 RVA: 0x0008D67C File Offset: 0x0008B87C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[343] && !PermanentBuffPlayer.PermanentBuffsBools[13])
			{
				player.Player.biomeSight = true;
				player.Player.buffImmune[343] = true;
			}
		}
	}
}
