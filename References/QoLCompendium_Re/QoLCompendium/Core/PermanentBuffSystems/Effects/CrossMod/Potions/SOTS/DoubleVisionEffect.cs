using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS
{
	// Token: 0x020002F5 RID: 757
	public class DoubleVisionEffect : IPermanentModdedBuff
	{
		// Token: 0x06001217 RID: 4631 RVA: 0x00089D98 File Offset: 0x00087F98
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "DoubleVision")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[3])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "DoubleVision"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "DoubleVision")] = true;
			}
		}
	}
}
