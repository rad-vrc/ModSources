using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks
{
	// Token: 0x02000390 RID: 912
	public class WeaponImbueFireEffect : IPermanentBuff
	{
		// Token: 0x0600134D RID: 4941 RVA: 0x0008E6D3 File Offset: 0x0008C8D3
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[74])
			{
				player.Player.meleeEnchant = 3;
				player.Player.buffImmune[74] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
