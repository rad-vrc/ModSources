using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity
{
	// Token: 0x02000352 RID: 850
	public class WeightlessCandleEffect : IPermanentModdedBuff
	{
		// Token: 0x060012D1 RID: 4817 RVA: 0x0008CEB8 File Offset: 0x0008B0B8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "BlueCandleBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[8])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "BlueCandleBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "BlueCandleBuff")] = true;
			}
		}
	}
}
