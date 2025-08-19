using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002E9 RID: 745
	public class SteadfastEffect : IPermanentModdedBuff
	{
		// Token: 0x060011FF RID: 4607 RVA: 0x0008975C File Offset: 0x0008795C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "TurtlePotionBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[13])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "TurtlePotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "TurtlePotionBuff")] = true;
			}
		}
	}
}
