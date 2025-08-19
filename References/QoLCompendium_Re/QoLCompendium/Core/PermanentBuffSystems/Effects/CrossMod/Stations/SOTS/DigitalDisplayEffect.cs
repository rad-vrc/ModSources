using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.SOTS
{
	// Token: 0x020002C3 RID: 707
	public class DigitalDisplayEffect : IPermanentModdedBuff
	{
		// Token: 0x060011B3 RID: 4531 RVA: 0x00088308 File Offset: 0x00086508
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "CyberneticEnhancements")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[10])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "CyberneticEnhancements"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "CyberneticEnhancements")] = true;
			}
		}
	}
}
