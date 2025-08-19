using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x0200030C RID: 780
	public class ZincPillEffect : IPermanentModdedBuff
	{
		// Token: 0x06001245 RID: 4677 RVA: 0x0008A9C0 File Offset: 0x00088BC0
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "ZincPillBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[16])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "ZincPillBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "ZincPillBuff")] = true;
			}
		}
	}
}
