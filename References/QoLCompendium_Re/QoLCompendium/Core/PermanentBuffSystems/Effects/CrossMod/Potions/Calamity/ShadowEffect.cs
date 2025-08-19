using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x02000320 RID: 800
	public class ShadowEffect : IPermanentModdedBuff
	{
		// Token: 0x0600126D RID: 4717 RVA: 0x0008B410 File Offset: 0x00089610
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "ShadowBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[19])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "ShadowBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "ShadowBuff")] = true;
			}
		}
	}
}
