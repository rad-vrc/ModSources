using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002D5 RID: 725
	public class FrenzyEffect : IPermanentModdedBuff
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x00088C8C File Offset: 0x00086E8C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "FrenzyPotionBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[11])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "FrenzyPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "FrenzyPotionBuff")] = true;
			}
		}
	}
}
