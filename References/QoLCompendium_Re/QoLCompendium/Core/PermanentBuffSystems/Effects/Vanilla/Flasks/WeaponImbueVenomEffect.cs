using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks
{
	// Token: 0x02000395 RID: 917
	public class WeaponImbueVenomEffect : IPermanentBuff
	{
		// Token: 0x06001357 RID: 4951 RVA: 0x0008E7EB File Offset: 0x0008C9EB
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[71])
			{
				player.Player.meleeEnchant = 1;
				player.Player.buffImmune[71] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
