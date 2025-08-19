using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks
{
	// Token: 0x0200038E RID: 910
	public class WeaponImbueConfettiEffect : IPermanentBuff
	{
		// Token: 0x06001349 RID: 4937 RVA: 0x0008E663 File Offset: 0x0008C863
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[78])
			{
				player.Player.meleeEnchant = 7;
				player.Player.buffImmune[78] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
