using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x02000308 RID: 776
	public class StarreachEffect : IPermanentModdedBuff
	{
		// Token: 0x0600123D RID: 4669 RVA: 0x0008A790 File Offset: 0x00088990
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "Starreach")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[12])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "Starreach"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "Starreach")] = true;
			}
		}
	}
}
