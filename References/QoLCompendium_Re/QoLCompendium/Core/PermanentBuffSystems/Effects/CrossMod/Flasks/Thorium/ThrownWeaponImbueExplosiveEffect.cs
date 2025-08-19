using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Thorium
{
	// Token: 0x0200033D RID: 829
	public class ThrownWeaponImbueExplosiveEffect : IPermanentModdedBuff
	{
		// Token: 0x060012A7 RID: 4775 RVA: 0x0008C370 File Offset: 0x0008A570
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ExplosiveCoatingBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ExplosiveCoatingBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ExplosiveCoatingBuff")] = true;
				Common.HandleCoatingBuffs(player.Player);
			}
		}
	}
}
