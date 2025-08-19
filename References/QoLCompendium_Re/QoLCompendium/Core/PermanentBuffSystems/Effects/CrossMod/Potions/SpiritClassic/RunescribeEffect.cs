using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002E3 RID: 739
	public class RunescribeEffect : IPermanentModdedBuff
	{
		// Token: 0x060011F3 RID: 4595 RVA: 0x0008941C File Offset: 0x0008761C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "RunePotionBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[7])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "RunePotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "RunePotionBuff")] = true;
			}
		}
	}
}
