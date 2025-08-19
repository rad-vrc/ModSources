using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002E5 RID: 741
	public class SoulguardEffect : IPermanentModdedBuff
	{
		// Token: 0x060011F7 RID: 4599 RVA: 0x00089530 File Offset: 0x00087730
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "SoulPotionBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[8])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "SoulPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "SoulPotionBuff")] = true;
			}
		}
	}
}
