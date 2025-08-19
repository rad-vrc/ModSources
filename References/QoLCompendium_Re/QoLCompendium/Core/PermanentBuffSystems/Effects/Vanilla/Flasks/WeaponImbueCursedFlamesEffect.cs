using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks
{
	// Token: 0x0200038F RID: 911
	public class WeaponImbueCursedFlamesEffect : IPermanentBuff
	{
		// Token: 0x0600134B RID: 4939 RVA: 0x0008E69B File Offset: 0x0008C89B
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[73])
			{
				player.Player.meleeEnchant = 2;
				player.Player.buffImmune[73] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
