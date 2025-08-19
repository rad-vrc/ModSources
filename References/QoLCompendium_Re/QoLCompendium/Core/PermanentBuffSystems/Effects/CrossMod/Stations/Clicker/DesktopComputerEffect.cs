using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.Clicker
{
	// Token: 0x020002C6 RID: 710
	public class DesktopComputerEffect : IPermanentModdedBuff
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x000884AC File Offset: 0x000866AC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.clickerClassLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.clickerClassMod, "DesktopComputerBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.clickerClassMod, "DesktopComputerBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.clickerClassMod, "DesktopComputerBuff")] = true;
			}
		}
	}
}
