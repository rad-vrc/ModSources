using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity
{
	// Token: 0x0200034A RID: 842
	public class ChaosCandleEffect : IPermanentModdedBuff
	{
		// Token: 0x060012C1 RID: 4801 RVA: 0x0008CA78 File Offset: 0x0008AC78
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[0])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "ChaosCandleBuff")] = true;
			}
		}
	}
}
