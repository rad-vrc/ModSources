using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002E4 RID: 740
	public class SoaringEffect : IPermanentModdedBuff
	{
		// Token: 0x060011F5 RID: 4597 RVA: 0x000894A4 File Offset: 0x000876A4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "FlightPotionBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[10])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "FlightPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "FlightPotionBuff")] = true;
			}
		}
	}
}
