using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.CalamityEntropy
{
	// Token: 0x02000315 RID: 789
	public class YharimsStimulantsEffect : IPermanentModdedBuff
	{
		// Token: 0x06001257 RID: 4695 RVA: 0x0008AE0C File Offset: 0x0008900C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityEntropyLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityEntropyMod, "YharimPower")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[32])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityEntropyMod, "YharimPower"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityEntropyMod, "YharimPower")] = true;
			}
		}
	}
}
