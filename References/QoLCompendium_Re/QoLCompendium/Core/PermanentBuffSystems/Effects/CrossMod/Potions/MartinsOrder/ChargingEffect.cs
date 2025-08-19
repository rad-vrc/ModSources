using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x020002FD RID: 765
	public class ChargingEffect : IPermanentModdedBuff
	{
		// Token: 0x06001227 RID: 4647 RVA: 0x0008A1AC File Offset: 0x000883AC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "Charging")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[1])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "Charging"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "Charging")] = true;
			}
		}
	}
}
