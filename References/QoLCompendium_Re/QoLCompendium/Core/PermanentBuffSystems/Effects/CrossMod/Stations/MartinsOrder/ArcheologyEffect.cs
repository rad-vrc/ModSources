using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.MartinsOrder
{
	// Token: 0x020002C4 RID: 708
	public class ArcheologyEffect : IPermanentModdedBuff
	{
		// Token: 0x060011B5 RID: 4533 RVA: 0x00088394 File Offset: 0x00086594
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "ReschBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[17])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "ReschBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "ReschBuff")] = true;
			}
		}
	}
}
