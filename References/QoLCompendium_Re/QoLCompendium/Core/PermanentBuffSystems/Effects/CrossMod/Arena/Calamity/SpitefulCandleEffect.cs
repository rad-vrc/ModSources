using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity
{
	// Token: 0x0200034F RID: 847
	public class SpitefulCandleEffect : IPermanentModdedBuff
	{
		// Token: 0x060012CB RID: 4811 RVA: 0x0008CD20 File Offset: 0x0008AF20
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "YellowCandleBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[5])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "YellowCandleBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "YellowCandleBuff")] = true;
			}
		}
	}
}
