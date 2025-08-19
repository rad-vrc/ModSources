using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Catalyst
{
	// Token: 0x02000313 RID: 787
	public class AstraJellyEffect : IPermanentModdedBuff
	{
		// Token: 0x06001253 RID: 4691 RVA: 0x0008ACF4 File Offset: 0x00088EF4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.catalystLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[25])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.catalystMod, "AstralJellyBuff")] = true;
			}
		}
	}
}
