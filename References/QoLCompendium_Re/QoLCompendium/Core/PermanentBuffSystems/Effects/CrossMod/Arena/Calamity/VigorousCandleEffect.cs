using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity
{
	// Token: 0x02000351 RID: 849
	public class VigorousCandleEffect : IPermanentModdedBuff
	{
		// Token: 0x060012CF RID: 4815 RVA: 0x0008CE30 File Offset: 0x0008B030
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "PinkCandleBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[7])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "PinkCandleBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "PinkCandleBuff")] = true;
			}
		}
	}
}
