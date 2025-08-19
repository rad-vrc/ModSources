using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic.Candies
{
	// Token: 0x020002ED RID: 749
	public class ChocolateBarEffect : IPermanentModdedBuff
	{
		// Token: 0x06001207 RID: 4615 RVA: 0x00089980 File Offset: 0x00087B80
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "ChocolateBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "ChocolateBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "ChocolateBuff")] = true;
			}
		}
	}
}
