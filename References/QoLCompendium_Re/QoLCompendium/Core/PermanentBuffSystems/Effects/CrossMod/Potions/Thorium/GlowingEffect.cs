using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002D6 RID: 726
	public class GlowingEffect : IPermanentModdedBuff
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x00088D18 File Offset: 0x00086F18
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "GlowingPotionBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[12])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "GlowingPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "GlowingPotionBuff")] = true;
			}
		}
	}
}
