using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity.Alchohols
{
	// Token: 0x0200033A RID: 826
	public class WhiskeyEffect : IPermanentModdedBuff
	{
		// Token: 0x060012A1 RID: 4769 RVA: 0x0008C1E4 File Offset: 0x0008A3E4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "WhiskeyBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "WhiskeyBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "WhiskeyBuff")] = true;
			}
		}
	}
}
