using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002DB RID: 731
	public class KineticEffect : IPermanentModdedBuff
	{
		// Token: 0x060011E3 RID: 4579 RVA: 0x00088FD4 File Offset: 0x000871D4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "KineticPotionBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[16])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "KineticPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "KineticPotionBuff")] = true;
			}
		}
	}
}
