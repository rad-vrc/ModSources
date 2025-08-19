using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS
{
	// Token: 0x020002F2 RID: 754
	public class AssassinationEffect : IPermanentModdedBuff
	{
		// Token: 0x06001211 RID: 4625 RVA: 0x00089C00 File Offset: 0x00087E00
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Assassination")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[0])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Assassination"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Assassination")] = true;
			}
		}
	}
}
