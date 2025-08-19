using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002DA RID: 730
	public class InspirationalReachEffect : IPermanentModdedBuff
	{
		// Token: 0x060011E1 RID: 4577 RVA: 0x00088F48 File Offset: 0x00087148
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "InspirationReachPotionBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[15])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "InspirationReachPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "InspirationReachPotionBuff")] = true;
			}
		}
	}
}
