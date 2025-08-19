using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.SpiritClassic
{
	// Token: 0x02000346 RID: 838
	public class KoiTotemEffect : IPermanentModdedBuff
	{
		// Token: 0x060012B9 RID: 4793 RVA: 0x0008C854 File Offset: 0x0008AA54
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "KoiTotemBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[1])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "KoiTotemBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "KoiTotemBuff")] = true;
			}
		}
	}
}
