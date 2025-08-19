using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x02000301 RID: 769
	public class GourmetFlavorEffect : IPermanentModdedBuff
	{
		// Token: 0x0600122F RID: 4655 RVA: 0x0008A3CC File Offset: 0x000885CC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "Gourmet")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[5])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "Gourmet"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "Gourmet")] = true;
			}
		}
	}
}
