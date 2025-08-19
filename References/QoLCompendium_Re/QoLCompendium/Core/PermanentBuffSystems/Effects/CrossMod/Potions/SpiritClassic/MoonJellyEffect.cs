using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002E2 RID: 738
	public class MoonJellyEffect : IPermanentModdedBuff
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x00089394 File Offset: 0x00087594
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "MoonBlessing")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[6])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "MoonBlessing"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "MoonBlessing")] = true;
			}
		}
	}
}
