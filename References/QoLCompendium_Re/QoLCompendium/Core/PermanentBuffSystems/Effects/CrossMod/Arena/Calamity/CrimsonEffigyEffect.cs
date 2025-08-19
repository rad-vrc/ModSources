using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity
{
	// Token: 0x0200034C RID: 844
	public class CrimsonEffigyEffect : IPermanentModdedBuff
	{
		// Token: 0x060012C5 RID: 4805 RVA: 0x0008CB88 File Offset: 0x0008AD88
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "CrimsonEffigyBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[2])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "CrimsonEffigyBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "CrimsonEffigyBuff")] = true;
			}
		}
	}
}
