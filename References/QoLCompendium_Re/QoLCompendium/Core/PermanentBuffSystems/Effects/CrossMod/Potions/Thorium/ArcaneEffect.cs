using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002CA RID: 714
	public class ArcaneEffect : IPermanentModdedBuff
	{
		// Token: 0x060011C1 RID: 4545 RVA: 0x000886A4 File Offset: 0x000868A4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ArcanePotionBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[2])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ArcanePotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ArcanePotionBuff")] = true;
			}
		}
	}
}
