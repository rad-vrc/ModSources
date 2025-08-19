using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002CC RID: 716
	public class AssassinEffect : IPermanentModdedBuff
	{
		// Token: 0x060011C5 RID: 4549 RVA: 0x000887B4 File Offset: 0x000869B4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "AssassinPotionBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[4])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "AssassinPotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "AssassinPotionBuff")] = true;
			}
		}
	}
}
