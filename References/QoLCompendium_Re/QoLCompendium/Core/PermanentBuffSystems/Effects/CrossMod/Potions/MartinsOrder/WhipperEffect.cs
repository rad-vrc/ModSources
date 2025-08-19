using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x0200030B RID: 779
	public class WhipperEffect : IPermanentModdedBuff
	{
		// Token: 0x06001243 RID: 4675 RVA: 0x0008A934 File Offset: 0x00088B34
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "WhipperBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[15])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "WhipperBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "WhipperBuff")] = true;
			}
		}
	}
}
