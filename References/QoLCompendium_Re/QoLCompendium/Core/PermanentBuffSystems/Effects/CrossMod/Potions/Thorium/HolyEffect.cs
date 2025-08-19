using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002D7 RID: 727
	public class HolyEffect : IPermanentModdedBuff
	{
		// Token: 0x060011DB RID: 4571 RVA: 0x00088DA4 File Offset: 0x00086FA4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "HolyPotionBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[13])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "HolyPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "HolyPotionBuff")] = true;
			}
		}
	}
}
