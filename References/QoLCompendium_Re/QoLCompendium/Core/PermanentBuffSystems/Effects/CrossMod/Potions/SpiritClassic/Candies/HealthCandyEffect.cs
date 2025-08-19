using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic.Candies
{
	// Token: 0x020002EE RID: 750
	public class HealthCandyEffect : IPermanentModdedBuff
	{
		// Token: 0x06001209 RID: 4617 RVA: 0x00089A00 File Offset: 0x00087C00
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "HealthBuffC")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "HealthBuffC"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "HealthBuffC")] = true;
			}
		}
	}
}
