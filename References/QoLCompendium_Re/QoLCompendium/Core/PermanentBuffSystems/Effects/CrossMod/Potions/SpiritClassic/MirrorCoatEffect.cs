using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002E1 RID: 737
	public class MirrorCoatEffect : IPermanentModdedBuff
	{
		// Token: 0x060011EF RID: 4591 RVA: 0x0008930C File Offset: 0x0008750C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "MirrorCoatBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[5])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "MirrorCoatBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "MirrorCoatBuff")] = true;
			}
		}
	}
}
