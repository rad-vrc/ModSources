using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.MartinsOrder
{
	// Token: 0x020002C5 RID: 709
	public class SporeFarmEffect : IPermanentModdedBuff
	{
		// Token: 0x060011B7 RID: 4535 RVA: 0x00088420 File Offset: 0x00086620
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "SporeSave")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[18])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "SporeSave"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "SporeSave")] = true;
			}
		}
	}
}
