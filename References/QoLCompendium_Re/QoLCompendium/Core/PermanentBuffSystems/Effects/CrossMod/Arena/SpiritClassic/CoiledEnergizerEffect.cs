using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.SpiritClassic
{
	// Token: 0x02000345 RID: 837
	public class CoiledEnergizerEffect : IPermanentModdedBuff
	{
		// Token: 0x060012B7 RID: 4791 RVA: 0x0008C7CC File Offset: 0x0008A9CC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "OverDrive")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[0])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "OverDrive"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "OverDrive")] = true;
			}
		}
	}
}
