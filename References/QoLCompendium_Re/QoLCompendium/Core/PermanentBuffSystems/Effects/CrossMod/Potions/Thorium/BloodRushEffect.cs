using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002CE RID: 718
	public class BloodRushEffect : IPermanentModdedBuff
	{
		// Token: 0x060011C9 RID: 4553 RVA: 0x000888C8 File Offset: 0x00086AC8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "BloodRush")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[5])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "BloodRush"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "BloodRush")] = true;
			}
		}
	}
}
