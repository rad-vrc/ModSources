using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Calamity
{
	// Token: 0x02000342 RID: 834
	public class WeaponImbueCrumblingEffect : IPermanentModdedBuff
	{
		// Token: 0x060012B1 RID: 4785 RVA: 0x0008C62C File Offset: 0x0008A82C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "WeaponImbueCrumbling")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "WeaponImbueCrumbling"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "WeaponImbueCrumbling")] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
