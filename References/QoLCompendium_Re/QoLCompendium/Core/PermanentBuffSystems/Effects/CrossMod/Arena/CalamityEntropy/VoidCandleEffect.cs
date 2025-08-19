using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.CalamityEntropy
{
	// Token: 0x02000349 RID: 841
	public class VoidCandleEffect : IPermanentModdedBuff
	{
		// Token: 0x060012BF RID: 4799 RVA: 0x0008C9EC File Offset: 0x0008ABEC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityEntropyLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityEntropyMod, "VoidCandleBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[30])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityEntropyMod, "VoidCandleBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityEntropyMod, "VoidCandleBuff")] = true;
			}
		}
	}
}
