using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x02000304 RID: 772
	public class RockskinEffect : IPermanentModdedBuff
	{
		// Token: 0x06001235 RID: 4661 RVA: 0x0008A564 File Offset: 0x00088764
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "RockskinBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[8])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "RockskinBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "RockskinBuff")] = true;
			}
		}
	}
}
