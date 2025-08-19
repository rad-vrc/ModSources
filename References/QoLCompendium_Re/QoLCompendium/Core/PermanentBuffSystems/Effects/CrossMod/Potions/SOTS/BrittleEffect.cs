using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS
{
	// Token: 0x020002F4 RID: 756
	public class BrittleEffect : IPermanentModdedBuff
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x00089D10 File Offset: 0x00087F10
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Brittle")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[2])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Brittle"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Brittle")] = true;
			}
		}
	}
}
