using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS
{
	// Token: 0x020002F9 RID: 761
	public class RoughskinEffect : IPermanentModdedBuff
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00089F88 File Offset: 0x00088188
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Roughskin")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[7])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Roughskin"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Roughskin")] = true;
			}
		}
	}
}
