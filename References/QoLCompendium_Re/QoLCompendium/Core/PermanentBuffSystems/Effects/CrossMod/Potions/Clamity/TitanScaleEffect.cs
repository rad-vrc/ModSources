using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Clamity
{
	// Token: 0x02000311 RID: 785
	public class TitanScaleEffect : IPermanentModdedBuff
	{
		// Token: 0x0600124F RID: 4687 RVA: 0x0008AC34 File Offset: 0x00088E34
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.clamityAddonLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.clamityAddonMod, "TitanScalePotionBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[29])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.clamityAddonMod, "TitanScalePotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.clamityAddonMod, "TitanScalePotionBuff")] = true;
			}
		}
	}
}
