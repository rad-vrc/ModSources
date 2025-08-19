using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS
{
	// Token: 0x020002F3 RID: 755
	public class BluefireEffect : IPermanentModdedBuff
	{
		// Token: 0x06001213 RID: 4627 RVA: 0x00089C88 File Offset: 0x00087E88
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Bluefire")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[1])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Bluefire"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Bluefire")] = true;
			}
		}
	}
}
