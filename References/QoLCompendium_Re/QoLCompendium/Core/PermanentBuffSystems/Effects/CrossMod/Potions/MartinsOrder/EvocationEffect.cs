using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x02000300 RID: 768
	public class EvocationEffect : IPermanentModdedBuff
	{
		// Token: 0x0600122D RID: 4653 RVA: 0x0008A344 File Offset: 0x00088544
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "SummonSpeedBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[4])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "SummonSpeedBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "SummonSpeedBuff")] = true;
			}
		}
	}
}
