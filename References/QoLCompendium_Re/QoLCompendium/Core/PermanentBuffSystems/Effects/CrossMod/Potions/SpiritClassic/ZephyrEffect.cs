using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002EB RID: 747
	public class ZephyrEffect : IPermanentModdedBuff
	{
		// Token: 0x06001203 RID: 4611 RVA: 0x00089874 File Offset: 0x00087A74
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "DoubleJumpPotionBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[15])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "DoubleJumpPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "DoubleJumpPotionBuff")] = true;
			}
		}
	}
}
