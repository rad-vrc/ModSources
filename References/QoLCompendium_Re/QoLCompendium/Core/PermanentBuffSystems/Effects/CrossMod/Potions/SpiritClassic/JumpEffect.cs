using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002E0 RID: 736
	public class JumpEffect : IPermanentModdedBuff
	{
		// Token: 0x060011ED RID: 4589 RVA: 0x00089284 File Offset: 0x00087484
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "PinkPotionBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[4])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "PinkPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "PinkPotionBuff")] = true;
			}
		}
	}
}
