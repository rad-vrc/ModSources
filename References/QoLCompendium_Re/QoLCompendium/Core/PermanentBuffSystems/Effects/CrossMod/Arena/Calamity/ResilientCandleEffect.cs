using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity
{
	// Token: 0x0200034E RID: 846
	public class ResilientCandleEffect : IPermanentModdedBuff
	{
		// Token: 0x060012C9 RID: 4809 RVA: 0x0008CC98 File Offset: 0x0008AE98
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "PurpleCandleBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[4])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "PurpleCandleBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "PurpleCandleBuff")] = true;
			}
		}
	}
}
