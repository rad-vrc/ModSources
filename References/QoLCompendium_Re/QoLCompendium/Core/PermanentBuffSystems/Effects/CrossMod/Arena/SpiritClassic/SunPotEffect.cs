using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.SpiritClassic
{
	// Token: 0x02000347 RID: 839
	public class SunPotEffect : IPermanentModdedBuff
	{
		// Token: 0x060012BB RID: 4795 RVA: 0x0008C8DC File Offset: 0x0008AADC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "SunPotBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[2])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "SunPotBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "SunPotBuff")] = true;
			}
		}
	}
}
