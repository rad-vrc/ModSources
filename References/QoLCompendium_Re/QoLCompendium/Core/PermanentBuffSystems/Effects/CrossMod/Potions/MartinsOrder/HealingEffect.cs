using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x02000303 RID: 771
	public class HealingEffect : IPermanentModdedBuff
	{
		// Token: 0x06001233 RID: 4659 RVA: 0x0008A4DC File Offset: 0x000886DC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "Healing")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[7])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "Healing"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "Healing")] = true;
			}
		}
	}
}
