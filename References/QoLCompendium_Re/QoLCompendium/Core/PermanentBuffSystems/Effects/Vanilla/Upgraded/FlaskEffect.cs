using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Flasks;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Upgraded
{
	// Token: 0x02000358 RID: 856
	public class FlaskEffect : IPermanentBuff
	{
		// Token: 0x060012DD RID: 4829 RVA: 0x0008D110 File Offset: 0x0008B310
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (player.Player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 0)
			{
				new WeaponImbueCursedFlamesEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 1)
			{
				new WeaponImbueFireEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 2)
			{
				new WeaponImbueGoldEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 3)
			{
				new WeaponImbueIchorEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 4)
			{
				new WeaponImbueNanitesEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 5)
			{
				new WeaponImbueConfettiEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 6)
			{
				new WeaponImbuePoisonEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().flaskEffectMode == 7)
			{
				new WeaponImbueVenomEffect().ApplyEffect(player);
			}
		}
	}
}
