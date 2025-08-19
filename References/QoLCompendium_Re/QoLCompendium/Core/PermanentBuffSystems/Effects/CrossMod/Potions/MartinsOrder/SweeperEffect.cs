using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x02000309 RID: 777
	public class SweeperEffect : IPermanentModdedBuff
	{
		// Token: 0x0600123F RID: 4671 RVA: 0x0008A81C File Offset: 0x00088A1C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "SweepBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[13])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "SweepBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "SweepBuff")] = true;
			}
		}
	}
}
