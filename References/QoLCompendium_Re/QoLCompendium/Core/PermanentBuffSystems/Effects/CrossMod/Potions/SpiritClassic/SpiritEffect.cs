using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002E6 RID: 742
	public class SpiritEffect : IPermanentModdedBuff
	{
		// Token: 0x060011F9 RID: 4601 RVA: 0x000895B8 File Offset: 0x000877B8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "SpiritBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[9])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "SpiritBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "SpiritBuff")] = true;
			}
		}
	}
}
