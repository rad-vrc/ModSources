using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Calamity
{
	// Token: 0x02000343 RID: 835
	public class WeaponImbueHolyFlamesEffect : IPermanentModdedBuff
	{
		// Token: 0x060012B3 RID: 4787 RVA: 0x0008C6B8 File Offset: 0x0008A8B8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "WeaponImbueHolyFlames")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "WeaponImbueHolyFlames"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "WeaponImbueHolyFlames")] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
