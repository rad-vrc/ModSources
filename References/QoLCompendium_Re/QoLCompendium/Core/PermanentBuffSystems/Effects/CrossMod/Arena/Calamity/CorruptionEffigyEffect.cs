using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity
{
	// Token: 0x0200034B RID: 843
	public class CorruptionEffigyEffect : IPermanentModdedBuff
	{
		// Token: 0x060012C3 RID: 4803 RVA: 0x0008CB00 File Offset: 0x0008AD00
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "CorruptionEffigyBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[1])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "CorruptionEffigyBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "CorruptionEffigyBuff")] = true;
			}
		}
	}
}
