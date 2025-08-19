using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Calamity
{
	// Token: 0x02000341 RID: 833
	public class WeaponImbueBrimstoneEffect : IPermanentModdedBuff
	{
		// Token: 0x060012AF RID: 4783 RVA: 0x0008C5A0 File Offset: 0x0008A7A0
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "WeaponImbueBrimstone")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "WeaponImbueBrimstone"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "WeaponImbueBrimstone")] = true;
				Common.HandleFlaskBuffs(player.Player);
			}
		}
	}
}
