using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS
{
	// Token: 0x020002F6 RID: 758
	public class HarmonyEffect : IPermanentModdedBuff
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x00089E20 File Offset: 0x00088020
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Harmony")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[4])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Harmony"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "Harmony")] = true;
			}
		}
	}
}
