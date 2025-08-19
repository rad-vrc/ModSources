using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks
{
	// Token: 0x02000393 RID: 915
	public class WeaponImbueNanitesEffect : IPermanentBuff
	{
		// Token: 0x06001353 RID: 4947 RVA: 0x0008E77B File Offset: 0x0008C97B
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[77])
			{
				player.Player.meleeEnchant = 6;
				player.Player.buffImmune[77] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
