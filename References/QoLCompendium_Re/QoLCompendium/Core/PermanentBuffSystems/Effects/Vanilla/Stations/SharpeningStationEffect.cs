using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations
{
	// Token: 0x02000361 RID: 865
	public class SharpeningStationEffect : IPermanentBuff
	{
		// Token: 0x060012EF RID: 4847 RVA: 0x0008D49C File Offset: 0x0008B69C
		internal unsafe override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[159] && !PermanentBuffPlayer.PermanentBuffsBools[55])
			{
				*player.Player.GetArmorPenetration(DamageClass.Melee) += 12f;
				player.Player.buffImmune[159] = true;
			}
		}
	}
}
