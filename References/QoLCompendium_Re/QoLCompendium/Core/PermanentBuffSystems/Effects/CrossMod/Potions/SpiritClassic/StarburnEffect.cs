using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002E8 RID: 744
	public class StarburnEffect : IPermanentModdedBuff
	{
		// Token: 0x060011FD RID: 4605 RVA: 0x000896D0 File Offset: 0x000878D0
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "StarPotionBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[12])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "StarPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "StarPotionBuff")] = true;
			}
		}
	}
}
