using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks
{
	// Token: 0x02000391 RID: 913
	public class WeaponImbueGoldEffect : IPermanentBuff
	{
		// Token: 0x0600134F RID: 4943 RVA: 0x0008E70B File Offset: 0x0008C90B
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[75])
			{
				player.Player.meleeEnchant = 4;
				player.Player.buffImmune[75] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
