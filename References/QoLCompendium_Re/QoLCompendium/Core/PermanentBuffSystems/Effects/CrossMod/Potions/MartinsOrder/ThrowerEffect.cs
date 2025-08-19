using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x0200030A RID: 778
	public class ThrowerEffect : IPermanentModdedBuff
	{
		// Token: 0x06001241 RID: 4673 RVA: 0x0008A8A8 File Offset: 0x00088AA8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "ThrowerBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[14])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "ThrowerBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "ThrowerBuff")] = true;
			}
		}
	}
}
