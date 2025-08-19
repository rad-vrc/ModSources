using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS
{
	// Token: 0x020002FB RID: 763
	public class VibeEffect : IPermanentModdedBuff
	{
		// Token: 0x06001223 RID: 4643 RVA: 0x0008A098 File Offset: 0x00088298
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "GoodVibes")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[9])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "GoodVibes"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "GoodVibes")] = true;
			}
		}
	}
}
