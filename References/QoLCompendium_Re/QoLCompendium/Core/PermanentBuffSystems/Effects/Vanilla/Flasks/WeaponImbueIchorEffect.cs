using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks
{
	// Token: 0x02000392 RID: 914
	public class WeaponImbueIchorEffect : IPermanentBuff
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x0008E743 File Offset: 0x0008C943
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[76])
			{
				player.Player.meleeEnchant = 5;
				player.Player.buffImmune[76] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
