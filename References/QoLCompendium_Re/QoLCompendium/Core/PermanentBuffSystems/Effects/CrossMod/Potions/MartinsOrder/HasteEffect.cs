using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x02000302 RID: 770
	public class HasteEffect : IPermanentModdedBuff
	{
		// Token: 0x06001231 RID: 4657 RVA: 0x0008A454 File Offset: 0x00088654
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "HasteBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[6])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "HasteBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "HasteBuff")] = true;
			}
		}
	}
}
