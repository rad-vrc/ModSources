using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity
{
	// Token: 0x02000350 RID: 848
	public class TranquilityCandleEffect : IPermanentModdedBuff
	{
		// Token: 0x060012CD RID: 4813 RVA: 0x0008CDA8 File Offset: 0x0008AFA8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "TranquilityCandleBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[6])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "TranquilityCandleBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "TranquilityCandleBuff")] = true;
			}
		}
	}
}
