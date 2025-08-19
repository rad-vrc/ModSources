using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS
{
	// Token: 0x020002FA RID: 762
	public class SoulAccessEffect : IPermanentModdedBuff
	{
		// Token: 0x06001221 RID: 4641 RVA: 0x0008A010 File Offset: 0x00088210
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "SoulAccess")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[8])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "SoulAccess"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "SoulAccess")] = true;
			}
		}
	}
}
