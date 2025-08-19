using System;
using QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Thorium;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Upgraded.Thorium
{
	// Token: 0x020002A4 RID: 676
	public class ThoriumCoatingEffect : IPermanentModdedBuff
	{
		// Token: 0x06001175 RID: 4469 RVA: 0x00087978 File Offset: 0x00085B78
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 0)
			{
				new ThrownWeaponImbueDeepFreezeEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 1)
			{
				new ThrownWeaponImbueExplosiveEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 2)
			{
				new ThrownWeaponImbueGorgonJuiceEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 3)
			{
				new ThrownWeaponImbueSporesEffect().ApplyEffect(player);
			}
			if (player.Player.GetModPlayer<QoLCPlayer>().thoriumCoatingMode == 4)
			{
				new ThrownWeaponImbueToxicEffect().ApplyEffect(player);
			}
		}
	}
}
