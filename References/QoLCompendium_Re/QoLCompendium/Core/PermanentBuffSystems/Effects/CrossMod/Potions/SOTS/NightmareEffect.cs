using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS
{
	// Token: 0x020002F7 RID: 759
	public class NightmareEffect : IPermanentModdedBuff
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x00089EA8 File Offset: 0x000880A8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Nightmare")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[5])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Nightmare"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Nightmare")] = true;
			}
		}
	}
}
