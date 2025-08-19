using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002D3 RID: 723
	public class EarwormEffect : IPermanentModdedBuff
	{
		// Token: 0x060011D3 RID: 4563 RVA: 0x00088B74 File Offset: 0x00086D74
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "EarwormPotionBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[10])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "EarwormPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "EarwormPotionBuff")] = true;
			}
		}
	}
}
