using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002CF RID: 719
	public class BouncingFlameEffect : IPermanentModdedBuff
	{
		// Token: 0x060011CB RID: 4555 RVA: 0x00088950 File Offset: 0x00086B50
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "BouncingFlamePotionBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[6])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "BouncingFlamePotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "BouncingFlamePotionBuff")] = true;
			}
		}
	}
}
