using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks
{
	// Token: 0x02000394 RID: 916
	public class WeaponImbuePoisonEffect : IPermanentBuff
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x0008E7B3 File Offset: 0x0008C9B3
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[79])
			{
				player.Player.meleeEnchant = 8;
				player.Player.buffImmune[79] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
