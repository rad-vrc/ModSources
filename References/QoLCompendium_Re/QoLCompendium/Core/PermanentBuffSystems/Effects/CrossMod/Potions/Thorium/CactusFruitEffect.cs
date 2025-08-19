using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002D0 RID: 720
	public class CactusFruitEffect : IPermanentModdedBuff
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x000889D8 File Offset: 0x00086BD8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "CactusFruitBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[7])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "CactusFruitBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "CactusFruitBuff")] = true;
			}
		}
	}
}
