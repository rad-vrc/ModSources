using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Thorium
{
	// Token: 0x0200033E RID: 830
	public class ThrownWeaponImbueGorgonJuiceEffect : IPermanentModdedBuff
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x0008C3FC File Offset: 0x0008A5FC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "GorgonCoatingBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "GorgonCoatingBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "GorgonCoatingBuff")] = true;
				Common.HandleCoatingBuffs(player.Player);
			}
		}
	}
}
