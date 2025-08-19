using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002D1 RID: 721
	public class ConflagrationEffect : IPermanentModdedBuff
	{
		// Token: 0x060011CF RID: 4559 RVA: 0x00088A60 File Offset: 0x00086C60
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ConflagrationPotionBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[8])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ConflagrationPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ConflagrationPotionBuff")] = true;
			}
		}
	}
}
