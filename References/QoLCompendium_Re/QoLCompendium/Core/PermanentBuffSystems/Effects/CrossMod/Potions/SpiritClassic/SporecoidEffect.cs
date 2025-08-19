using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002E7 RID: 743
	public class SporecoidEffect : IPermanentModdedBuff
	{
		// Token: 0x060011FB RID: 4603 RVA: 0x00089644 File Offset: 0x00087844
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "MushroomPotionBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[11])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "MushroomPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "MushroomPotionBuff")] = true;
			}
		}
	}
}
